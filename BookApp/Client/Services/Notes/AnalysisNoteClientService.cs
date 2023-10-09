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

        public Task<HttpResponseMessage> DeleteNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EditNote(AnalysisNoteModel noteModel)
        {
            throw new NotImplementedException("Service reached!");
        }
    }
}
