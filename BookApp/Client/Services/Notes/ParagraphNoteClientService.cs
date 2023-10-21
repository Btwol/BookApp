using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;

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

        public async Task<HttpResponseMessage> AddNote(ParagraphNoteModel paragraphNoteModel)
        {
            return await Http.PostAsJsonAsync($"ParagraphNote/AddParagraphNote", paragraphNoteModel);
        }

        public async Task<HttpResponseMessage> DeleteNote(int noteId)
        {
            return await Http.DeleteAsync($"ParagraphNote/DeleteParagraphNote/{noteId}");
        }

        public async Task<HttpResponseMessage> EditNote(ParagraphNoteModel paragraphNoteModel)
        {
            return await Http.PutAsJsonAsync($"ParagraphNote/EditParagraphNote", paragraphNoteModel);
        }

        public async Task<HttpResponseMessage> AddTag(int paragraphNoteId, int tagId)
        {
            return await Http.PostAsync($"ParagraphNote/AddTag/{paragraphNoteId}/{tagId}", null);
        }

        public async Task<HttpResponseMessage> RemoveTag(int paragraphNoteId, int tagId)
        {
            return await Http.DeleteAsync($"ParagraphNote/RemoveTag/{paragraphNoteId}/{tagId}");
        }
    }
}
