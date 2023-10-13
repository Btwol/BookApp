using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;

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

        public async Task<HttpResponseMessage> AddNote(AnalysisNoteModel analysisNoteModel)
        {
            return await Http.PostAsJsonAsync($"AnalysisNote/AddAnalysisNote", analysisNoteModel);
        }

        public async Task<HttpResponseMessage> DeleteNote(int noteId)
        {
            return await Http.DeleteAsync($"AnalysisNote/DeleteAnalysisNote/{noteId}");
        }

        public async Task<HttpResponseMessage> EditNote(AnalysisNoteModel analysisNoteModel)
        {
            return await Http.PutAsJsonAsync($"AnalysisNote/EditAnalysisNote", analysisNoteModel);
        }

        public async Task<HttpResponseMessage> AddTag(int analysisNoteId, int tagId)
        {
            return await Http.PostAsync($"AnalysisNote/AddTag/{analysisNoteId}/{tagId}", null);
        }

        public async Task<HttpResponseMessage> RemoveTag(int analysisNoteId, int tagId)
        {
            return await Http.DeleteAsync($"AnalysisNote/RemoveTag/{analysisNoteId}/{tagId}");
        }
    }
}
