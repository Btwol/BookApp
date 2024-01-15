using BookApp.Client.Components;
using BookApp.Client.Interfaces;
using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.AspNetCore.SignalR.Client;

namespace BookApp.Client.Services
{
    public class HubClientService : IHubClientService
    {
        private readonly IAppStorage _appStorage;
        public HubConnection hubConnection { get; private set; }

        public HubClientService(IAppStorage appStorage, HubConnection hubConnection)
        {
            _appStorage = appStorage;
            this.hubConnection = hubConnection;
        }

        public async Task JoinAnalysisEditGroup(string? bookAnalysisId)
        {
            if (hubConnection.State != HubConnectionState.Connected)
            {
                await hubConnection.StartAsync();
            }
            bookAnalysisId = string.IsNullOrEmpty(bookAnalysisId) ? await _appStorage.GetStoredBookAnalysisId() : bookAnalysisId;
            await hubConnection.SendAsync("JoinAnalysisEditGroup", bookAnalysisId);
        }

        public async Task LeaveAnalysisEditGroup()
        {
            if (hubConnection.State != HubConnectionState.Disconnected)
            {
                var bookAnalysisId = await _appStorage.GetStoredBookAnalysisId();
                if (!string.IsNullOrEmpty(bookAnalysisId))
                {
                    await hubConnection.SendAsync("LeaveAnalysisEditGroup", bookAnalysisId);
                }
            }
        }

        public async Task RegisterReaderHub(IAnalysisComponent analysisComponent)
        {
            await JoinAnalysisEditGroup(analysisComponent.BookAnalysis.Id.ToString());

            hubConnection.On("HighlightAdded", async (HighlightModel highlight) =>
            {
                analysisComponent.BookAnalysis.Highlights.Add(highlight);
                await analysisComponent.ReRender();
            });

            hubConnection.On("HighlightUpdated", async (HighlightModel highlight) =>
            {
                var highlightToEdit = analysisComponent.BookAnalysis.Highlights.FirstOrDefault(h => h.Id == highlight.Id);
                if (highlightToEdit is not null)
                {
                    analysisComponent.BookAnalysis.Highlights.Remove(highlightToEdit);
                    analysisComponent.BookAnalysis.Highlights.Add(highlight);
                    await analysisComponent.ReRender();
                }
            });

            hubConnection.On("HighlightRemoved", async (int highlightId) =>
            {
                var highlightToDelete = analysisComponent.BookAnalysis.Highlights.FirstOrDefault(h => h.Id == highlightId);
                if (highlightToDelete is not null)
                {
                    analysisComponent.BookAnalysis.Highlights.Remove(highlightToDelete);
                    await analysisComponent.ReRender();
                }
            });

            hubConnection.On("AnalysisNoteCreated", async (AnalysisNoteModel noteModel) =>
            {
                AddNote(analysisComponent.BookAnalysis.AnalysisNotes, noteModel, analysisComponent);
            });

            hubConnection.On("ChapterNoteCreated", async (ChapterNoteModel noteModel) =>
            {
                AddNote(analysisComponent.BookAnalysis.ChapterNotes, noteModel, analysisComponent);
            });

            hubConnection.On("ParagraphNoteCreated", async (ParagraphNoteModel noteModel) =>
            {
                AddNote(analysisComponent.BookAnalysis.ParagraphNotes, noteModel, analysisComponent);
            });

            hubConnection.On("HighlightNoteCreated", async (HighlightNoteModel noteModel) =>
            {
                var highlight = analysisComponent.BookAnalysis.Highlights.FirstOrDefault(h => h.Id == noteModel.HighlightId);
                if (highlight is not null)
                {
                    AddNote(highlight.HighlightNotes, noteModel, analysisComponent);
                }
            });

            hubConnection.On("AnalysisNoteUpdated", async (AnalysisNoteModel noteModel) =>
            {
                UpdateNote(analysisComponent.BookAnalysis.AnalysisNotes, noteModel, analysisComponent);
            });

            hubConnection.On("ChapterNoteUpdated", async (ChapterNoteModel noteModel) =>
            {
                UpdateNote(analysisComponent.BookAnalysis.ChapterNotes, noteModel, analysisComponent);
            });

            hubConnection.On("ParagraphNoteUpdated", async (ParagraphNoteModel noteModel) =>
            {
                UpdateNote(analysisComponent.BookAnalysis.ParagraphNotes, noteModel, analysisComponent);
            });

            hubConnection.On("HighlightNoteUpdated", async (HighlightNoteModel noteModel) =>
            {
                var highlight = analysisComponent.BookAnalysis.Highlights.FirstOrDefault(h => h.Id == noteModel.HighlightId);
                if (highlight is not null)
                {
                    UpdateNote(highlight.HighlightNotes, noteModel, analysisComponent);
                }
            });

            hubConnection.On("AnalysisNoteDeleted", async (int noteId) =>
            {
                DeleteNote(analysisComponent.BookAnalysis.AnalysisNotes, noteId, analysisComponent);
            });

            hubConnection.On("ChapterNoteDeleted", async (int noteId) =>
            {
                DeleteNote(analysisComponent.BookAnalysis.ChapterNotes, noteId, analysisComponent);
            });

            hubConnection.On("ParagraphNoteDeleted", async (int noteId) =>
            {
                DeleteNote(analysisComponent.BookAnalysis.ParagraphNotes, noteId, analysisComponent);
            });

            hubConnection.On("HighlightNoteDeleted", async (int noteId) =>
            {
                var highlight = analysisComponent.BookAnalysis.Highlights.FirstOrDefault(h => h.HighlightNotes.Any(n => n.Id == noteId));
                if (highlight is not null)
                {
                    DeleteNote(highlight.HighlightNotes, noteId, analysisComponent);
                }
            });

            hubConnection.On("TagCreated", async (TagModel tagModel) =>
            {
                analysisComponent.BookAnalysis.Tags.Add(tagModel);
                await analysisComponent.ReRender();
            });

            hubConnection.On("TagUpdated", async (TagModel tagModel) =>
            {
                var tagToUpdate = analysisComponent.BookAnalysis.Tags.FirstOrDefault(t => t.Id == tagModel.Id);
                if (tagToUpdate is not null)
                {
                    analysisComponent.BookAnalysis.Tags.Remove(tagToUpdate);
                    analysisComponent.BookAnalysis.Tags.Add(tagModel);
                    await analysisComponent.ReRender();
                }
            });

            hubConnection.On("TagDeleted", async (int tagId) =>
            {
                var tagToDelete = analysisComponent.BookAnalysis.Tags.FirstOrDefault(t => t.Id == tagId);
                if (tagToDelete is not null)
                {
                    analysisComponent.BookAnalysis.Tags.Remove(tagToDelete);
                    await analysisComponent.ReRender();
                }
            });

            hubConnection.On("TagAdded", async (int tagId, int taggedId, string taggedType) =>
            {
                ITagableItemModel taggedItem = GetTaggedItem(taggedType, taggedId, analysisComponent.BookAnalysis);

                var tag = analysisComponent.BookAnalysis.Tags.FirstOrDefault(t => t.Id == tagId);

                if (taggedItem is not null && tag is not null)
                {
                    AddTag(taggedItem, tag, analysisComponent);
                }
            });

            hubConnection.On("TagRemoved", async (int tagId, int taggedId, string taggedType) =>
            {
                ITagableItemModel taggedItem = GetTaggedItem(taggedType, taggedId, analysisComponent.BookAnalysis);

                var tag = analysisComponent.BookAnalysis.Tags.FirstOrDefault(t => t.Id == tagId);

                if (taggedItem is not null && tag is not null)
                {
                    RemoveTag(taggedItem, tag, analysisComponent);
                }
            });
        }

        private ITagableItemModel GetTaggedItem(string taggedType, int taggedId, BookAnalysisDetailedModel bookAnalysis)
        {
            ITagableItemModel taggedItem = null;
            Console.WriteLine(taggedType);
            switch (taggedType)
            {
                case "Highlight":
                    taggedItem = bookAnalysis.Highlights.FirstOrDefault(t => t.Id == taggedId);
                    break;
                case "HighlightNote":
                    taggedItem = bookAnalysis.Highlights.FirstOrDefault(t => t.HighlightNotes.Any(n => n.Id == taggedId)).HighlightNotes.FirstOrDefault(n => n.Id == taggedId);
                    break;
                case "AnalysisNote":
                    taggedItem = bookAnalysis.AnalysisNotes.FirstOrDefault(t => t.Id == taggedId);
                    break;
                case "ChapterNote":
                    taggedItem = bookAnalysis.ChapterNotes.FirstOrDefault(t => t.Id == taggedId);
                    break;
                case "ParagraphNote":
                    taggedItem = bookAnalysis.ParagraphNotes.FirstOrDefault(t => t.Id == taggedId);
                    break;
            }

            return taggedItem;
        }


        private void AddTag<T>(T taggedItem, TagModel tag, IAnalysisComponent analysisComponent) where T : ITagableItemModel
        {
            Console.WriteLine("1");
            taggedItem.Tags.Add(tag);
            analysisComponent.ReRender();
        }

        private void RemoveTag<T>(T taggedItem, TagModel tag, IAnalysisComponent analysisComponent) where T : ITagableItemModel
        {
            var tagToRemove = taggedItem.Tags.FirstOrDefault(t => t.Id == tag.Id);
            if (tagToRemove is not null)
            {
                taggedItem.Tags.Remove(tagToRemove);
                analysisComponent.ReRender();
            }
        }

        private void AddNote<T>(List<T> notes, T note, IAnalysisComponent analysisComponent) where T : INoteClientModel
        {
            notes.Add(note);
            analysisComponent.ReRender();
        }

        private void UpdateNote<T>(List<T> notes, T note, IAnalysisComponent analysisComponent) where T : INoteClientModel
        {
            var noteToUpdate = notes.FirstOrDefault(n => n.Id == note.Id);
            if (noteToUpdate is not null)
            {
                noteToUpdate.Content = note.Content;
                analysisComponent.ReRender();
            }
        }

        private void DeleteNote<T>(List<T> notes, int noteId, IAnalysisComponent analysisComponent) where T : INoteClientModel
        {
            var noteToDelete = notes.FirstOrDefault(n => n.Id == noteId);
            if (noteToDelete is not null)
            {
                notes.Remove(noteToDelete);
                analysisComponent.ReRender();
            }
        }
    }
}
