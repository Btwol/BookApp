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

        public async Task<ParagraphNoteModel> AddNote(ParagraphNoteModel paragraphNoteModel)
        {
            var response = await Http.PostAsJsonAsync($"ParagraphNote/AddParagraphNote", paragraphNoteModel);
            return await HelperService.HandleResponse<ParagraphNoteModel>(response);
        }

        public async Task DeleteNote(int noteId, int bookAnalysisId)
        {
            var response = await Http.DeleteAsync($"ParagraphNote/DeleteParagraphNote/{noteId}/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public async Task<ParagraphNoteModel> EditNote(ParagraphNoteModel paragraphNoteModel)
        {
            var response = await Http.PutAsJsonAsync($"ParagraphNote/EditParagraphNote", paragraphNoteModel);
            return await HelperService.HandleResponse<ParagraphNoteModel>(response);
        }

        public async Task AddTag(int paragraphNoteId, int tagId)
        {
            var response = await Http.PostAsync($"ParagraphNote/AddTag/{paragraphNoteId}/{tagId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task RemoveTag(int paragraphNoteId, int tagId)
        {
            var response = await Http.DeleteAsync($"ParagraphNote/RemoveTag/{paragraphNoteId}/{tagId}");
            await HelperService.HandleResponse(response);
        }
    }
}
