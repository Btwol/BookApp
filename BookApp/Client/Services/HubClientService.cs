using BookApp.Client.Components;
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
            bookAnalysisId = (string.IsNullOrEmpty(bookAnalysisId)) ? await _appStorage.GetStoredBookAnalysisId() : bookAnalysisId;
            await hubConnection.SendAsync("JoinAnalysisEditGroup", bookAnalysisId); //sends message back to server hub
        }

        public async Task LeaveAnalysisEditGroup()
        {
            if(hubConnection.State != HubConnectionState.Disconnected)
            {
                var bookAnalysisId = await _appStorage.GetStoredBookAnalysisId();
                if(!string.IsNullOrEmpty(bookAnalysisId))
                {
                    await hubConnection.SendAsync("LeaveAnalysisEditGroup", bookAnalysisId); //sends message back to server hub
                }
            }
        }

        public async Task RegisterReaderHub(TextBox textBox)
        {
            await JoinAnalysisEditGroup(textBox.bookAnalysis.Id.ToString());

            hubConnection.On("HighlightAdded", async (HighlightModel highlight) =>
            {
                textBox.bookAnalysis.Highlights.Add(highlight);
                textBox.ReRender();
            });

            hubConnection.On("HighlightUpdated", async (HighlightModel highlight) =>
            {
                var highlightToEdit = textBox.bookAnalysis.Highlights.FirstOrDefault(h => h.Id == highlight.Id);
                if (highlightToEdit is not null)
                {
                    textBox.bookAnalysis.Highlights.Remove(highlightToEdit);
                    textBox.bookAnalysis.Highlights.Add(highlight);
                    textBox.ReRender();
                }
            });

            hubConnection.On("HighlightRemoved", async (int highlightId) =>
            {
                var highlightToDelete = textBox.bookAnalysis.Highlights.FirstOrDefault(h => h.Id == highlightId);
                if (highlightToDelete is not null)
                {
                    textBox.bookAnalysis.Highlights.Remove(highlightToDelete);
                    textBox.ReRender();
                }
            });

            hubConnection.On("AnalysisNoteCreated", async (AnalysisNoteModel noteModel) =>
            {
                AddNote(textBox.bookAnalysis.AnalysisNotes, noteModel, textBox);
            });

            hubConnection.On("ChapterNoteCreated", async (ChapterNoteModel noteModel) =>
            {
                AddNote(textBox.bookAnalysis.ChapterNotes, noteModel, textBox);
            });

            hubConnection.On("ParagraphNoteCreated", async (ParagraphNoteModel noteModel) =>
            {
                AddNote(textBox.bookAnalysis.ParagraphNotes, noteModel, textBox);
            });

            hubConnection.On("HighlightNoteCreated", async (HighlightNoteModel noteModel) =>
            {
                var highlight = textBox.bookAnalysis.Highlights.FirstOrDefault(h => h.Id == noteModel.HighlightId);
                if(highlight is not null)
                {
                    AddNote(highlight.HighlightNotes, noteModel, textBox);
                }
            });

            hubConnection.On("AnalysisNoteUpdated", async (AnalysisNoteModel noteModel) =>
            {
                UpdateNote(textBox.bookAnalysis.AnalysisNotes, noteModel, textBox);
            });

            hubConnection.On("ChapterNoteUpdated", async (ChapterNoteModel noteModel) =>
            {
                UpdateNote(textBox.bookAnalysis.ChapterNotes, noteModel, textBox);
            });

            hubConnection.On("ParagraphNoteUpdated", async (ParagraphNoteModel noteModel) =>
            {
                UpdateNote(textBox.bookAnalysis.ParagraphNotes, noteModel, textBox);
            });

            hubConnection.On("HighlightNoteUpdated", async (HighlightNoteModel noteModel) =>
            {
                var highlight = textBox.bookAnalysis.Highlights.FirstOrDefault(h => h.Id == noteModel.HighlightId);
                if (highlight is not null)
                {
                    UpdateNote(highlight.HighlightNotes, noteModel, textBox);
                }
            });

            hubConnection.On("AnalysisNoteDeleted", async (int noteId) =>
            {
                DeleteNote(textBox.bookAnalysis.AnalysisNotes, noteId, textBox);
            });

            hubConnection.On("ChapterNoteDeleted", async (int noteId) =>
            {
                DeleteNote(textBox.bookAnalysis.ChapterNotes, noteId, textBox);
            });

            hubConnection.On("ParagraphNoteDeleted", async (int noteId) =>
            {
                DeleteNote(textBox.bookAnalysis.ParagraphNotes, noteId, textBox);
            });

            hubConnection.On("HighlightNoteDeleted", async (int noteId) =>
            {
                var highlight = textBox.bookAnalysis.Highlights.FirstOrDefault(h => h.HighlightNotes.Any(n => n.Id == noteId));
                if (highlight is not null)
                {
                    DeleteNote(highlight.HighlightNotes, noteId, textBox);
                }
            });
        }

        private void AddNote<T>(List<T> notes, T note, TextBox textBox) where T : INoteClientModel
        {
            notes.Add(note);
            textBox.ReRender();
        }

        private void UpdateNote<T>(List<T> notes, T note, TextBox textBox) where T : INoteClientModel
        {
            var noteToUpdate = notes.FirstOrDefault(n => n.Id == note.Id);
            if (noteToUpdate is not null)
            {
                notes.Remove(noteToUpdate);
                notes.Add(note);
                textBox.ReRender();
            }
        }

        private void DeleteNote<T>(List<T> notes, int noteId, TextBox textBox) where T : INoteClientModel
        {
            var noteToDelete = notes.FirstOrDefault(n => n.Id == noteId);
            if (noteToDelete is not null)
            {
                notes.Remove(noteToDelete);
                textBox.ReRender();
            }
        }
    }
}
