using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Data;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BookApp.Client.Services
{
    public class BookAnalysisClientService : IBookAnalysisClientService
    {
        private readonly HttpClient Http;

        public BookAnalysisClientService(HttpClient http)
        {
            Http = http;
        }

        public async Task<ServiceResponse> CreateBookAnalysis(BookAnalysisModel newBookAnalysis)
        {
            var response = await Http.PostAsJsonAsync<BookAnalysisModel>("BookAnalysis/CreateBookAnalysis", newBookAnalysis);
            var content = await response.Content.ReadFromJsonAsync<ServiceResponse>();
            if (content.SuccessStatus)
            {
                return content;
            }
            else
            {
                throw new Exception(content.Message);
            }
        }

        public async Task<HttpResponseMessage> GetAnalysisByHash(string bookHash)
        {
            //return await Http.GetFromJsonAsync<List<BookAnalysisModel>>($"GetAnalysisByHash/{bookHash}");

            var response = await Http.GetAsync($"BookAnalysis/GetAnalysisByHash/{bookHash}");
            //return response;
            //return await response.Content.ReadFromJsonAsync<ServiceResponse>();

            return response;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("a");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                var content = await response.Content.ReadFromJsonAsync<ServiceResponse<List<BookAnalysisModel>>>();
                return response;
            }
            else
            {
                Console.WriteLine(response.Content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                var content = await response.Content.ReadFromJsonAsync<ServiceResponse>();
                throw new Exception(content.Message);
            }
        }

        public async Task<ServiceResponse> GetBookAnalysis(int analysisId)
        {
            //return await Http.GetFromJsonAsync<BookAnalysisModel>($"GetBookAnalysis/{analysisId}");

            var response = await Http.GetFromJsonAsync<ServiceResponse<BookAnalysisModel>>($"GetBookAnalysis/{analysisId}");
            if (response.SuccessStatus)
            {
                return response;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<ServiceResponse> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysis)
        {
            //var a = await Http.PutAsJsonAsync<BookAnalysisModel>("UpdateBookAnalysis", updatedBookAnalysis);
            return null;
        }

        public async Task Crash()
        {
            throw new Exception("Crash!");
        }
    }
}
