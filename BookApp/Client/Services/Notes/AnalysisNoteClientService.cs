using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using System.Net.Http.Json;

namespace BookApp.Client.Services.Notes
{
    public class AnalysisNoteClientService : IAnalysisNoteClientService
    {
        private readonly HttpClient Http;

        public AnalysisNoteClientService(HttpClient http)
        {
            Http = http;
        }

        public async Task<HttpResponseMessage> AddAnalysisNote(AnalysisNoteModel analysisNoteModel)
        {
            return await Http.PostAsJsonAsync($"AnalysisNote/AddAnalysisNote", analysisNoteModel);
        }
    }
}
