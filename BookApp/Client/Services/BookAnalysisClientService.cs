using BookApp.Shared;
using BookApp.Shared.Data;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BookApp.Client.Services
{
    public class BookAnalysisClientService
    {
        private readonly HttpClient Http;

        public BookAnalysisClientService(HttpClient http)
        {
            Http = http;
        }

        public async Task<BookAnalysis> GetBookAnalysis(Guid analysisId)
        {
            return null;
            //await Http.GetFromJsonAsync<BookAnalysis>("GetBookAnalysis");
        }
    }
}
