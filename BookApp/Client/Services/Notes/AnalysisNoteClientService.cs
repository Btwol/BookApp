using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Net.WebSockets;

namespace BookApp.Client.Services.Notes
{
    public class AnalysisNoteClientService : IAnalysisNoteClientService
    {
        private readonly HttpClient Http;

        public AnalysisNoteClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public async Task<AnalysisNoteModel> AddNote(AnalysisNoteModel analysisNoteModel)
        {
            var response = await Http.PostAsJsonAsync($"AnalysisNote/AddAnalysisNote", analysisNoteModel);
            return await HelperService.HandleResponse<AnalysisNoteModel>(response);
        }

        public async Task DeleteNote(int noteId, int bookAnalysisId)
        {
            var response = await Http.DeleteAsync($"AnalysisNote/DeleteAnalysisNote/{noteId}/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public async Task<AnalysisNoteModel> EditNote(AnalysisNoteModel analysisNoteModel)
        {
            var response = await Http.PutAsJsonAsync($"AnalysisNote/EditAnalysisNote", analysisNoteModel);
            return await HelperService.HandleResponse<AnalysisNoteModel>(response);
        }

        public async Task AddTag(int analysisNoteId, int tagId)
        {
            var response = await Http.PostAsync($"AnalysisNote/AddTag/{analysisNoteId}/{tagId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task RemoveTag(int analysisNoteId, int tagId)
        {
            var response = await Http.DeleteAsync($"AnalysisNote/RemoveTag/{analysisNoteId}/{tagId}");
            await HelperService.HandleResponse(response);
        }
    }
}
