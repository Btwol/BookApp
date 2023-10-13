using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services.Notes
{
    public class HighlightNoteClientService : IHighlightNoteClientService
    {
        private readonly HttpClient Http;

        public HighlightNoteClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public async Task<HttpResponseMessage> AddNote(HighlightNoteModel highlightNoteModel)
        {
            return await Http.PostAsJsonAsync($"HighlightNote/AddHighlightNote", highlightNoteModel);
        }

        public async Task<HttpResponseMessage> DeleteNote(int noteId)
        {
            return await Http.DeleteAsync($"HighlightNote/DeleteHighlightNote/{noteId}");
        }

        public async Task<HttpResponseMessage> EditNote(HighlightNoteModel highlightNoteModel)
        {
            return await Http.PutAsJsonAsync($"HighlightNote/EditHighlightNote", highlightNoteModel);
        }

        public async Task<HttpResponseMessage> AddTag(int highlightNoteId, int tagId)
        {
            return await Http.PostAsync($"HighlightNote/AddTag/{highlightNoteId}/{tagId}", null);
        }

        public async Task<HttpResponseMessage> RemoveTag(int highlightNoteId, int tagId)
        {
            return await Http.DeleteAsync($"HighlightNote/RemoveTag/{highlightNoteId}/{tagId}");
        }
    }
}
