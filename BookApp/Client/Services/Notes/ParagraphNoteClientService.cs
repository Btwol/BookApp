using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BookApp.Client.Services.Notes
{
    public class ParagraphNoteClientService : IParagraphNoteClientService
    {
        private readonly HttpClient Http;

        public ParagraphNoteClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public Task<HttpResponseMessage> AddNote(ParagraphNoteModel noteModel)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> AddParagraphNote(ParagraphNoteModel paragraphNoteModel)
        {
            return await Http.PostAsJsonAsync($"ParagraphNote/AddParagraphNote", paragraphNoteModel);
        }

        public Task<HttpResponseMessage> DeleteNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EditNote(NoteModel noteModel)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EditNote(ParagraphNoteModel noteModel)
        {
            throw new NotImplementedException();
        }
    }
}
