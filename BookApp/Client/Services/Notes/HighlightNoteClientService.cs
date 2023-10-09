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

        public async Task<HttpResponseMessage> AddHighlightNote(HighlightNoteModel highlightNoteModel)
        {
            return await Http.PostAsJsonAsync($"HighlightNote/AddHighlightNote", highlightNoteModel);
        }

        public Task<HttpResponseMessage> AddNote(HighlightNoteModel noteModel)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EditNote(NoteModel noteModel)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EditNote(HighlightNoteModel noteModel)
        {
            throw new NotImplementedException();
        }
    }
}
