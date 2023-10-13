using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services.Notes
{
    public class ChapterNoteClientService : IChapterNoteClientService
    {
        private readonly HttpClient Http;

        public ChapterNoteClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public async Task<HttpResponseMessage> AddNote(ChapterNoteModel chapterNoteModel)
        {
            return await Http.PostAsJsonAsync($"ChapterNote/AddChapterNote", chapterNoteModel);
        }

        public async Task<HttpResponseMessage> DeleteNote(int noteId)
        {
            return await Http.DeleteAsync($"ChapterNote/DeleteChapterNote/{noteId}");
        }

        public async Task<HttpResponseMessage> EditNote(ChapterNoteModel chapterNoteModel)
        {
            return await Http.PutAsJsonAsync($"ChapterNote/EditChapterNote", chapterNoteModel);
        }

        public async Task<HttpResponseMessage> AddTag(int chapterNoteId, int tagId)
        {
            return await Http.PostAsync($"ChapterNote/AddTag/{chapterNoteId}/{tagId}", null);
        }

        public async Task<HttpResponseMessage> RemoveTag(int chapterNoteId, int tagId)
        {
            return await Http.DeleteAsync($"ChapterNote/RemoveTag/{chapterNoteId}/{tagId}");
        }
    }
}
