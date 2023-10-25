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

        public async Task<HighlightNoteModel> AddNote(HighlightNoteModel highlightNoteModel)
        {
            var response = await Http.PostAsJsonAsync($"HighlightNote/AddHighlightNote", highlightNoteModel);
            return await HelperService.HandleResponse<HighlightNoteModel>(response);
        }

        public async Task DeleteNote(int noteId, int bookAnalysisId)
        {
            var response = await Http.DeleteAsync($"HighlightNote/DeleteHighlightNote/{noteId}/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public async Task<HighlightNoteModel> EditNote(HighlightNoteModel highlightNoteModel)
        {
            var response = await Http.PutAsJsonAsync($"HighlightNote/EditHighlightNote", highlightNoteModel);
            return await HelperService.HandleResponse<HighlightNoteModel>(response);
        }

        public async Task AddTag(int highlightNoteId, int tagId)
        {
            var response = await Http.PostAsync($"HighlightNote/AddTag/{highlightNoteId}/{tagId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task RemoveTag(int highlightNoteId, int tagId)
        {
            var response = await Http.DeleteAsync($"HighlightNote/RemoveTag/{highlightNoteId}/{tagId}");
            await HelperService.HandleResponse(response);
        }
    }

}
