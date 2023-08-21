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

        public async Task<List<BookAnalysisModel>> GetAnalysisByHash(string bookHash)
        {
            //return await Http.GetFromJsonAsync<List<BookAnalysisModel>>($"GetAnalysisByHash/{bookHash}");

            var response = await Http.GetAsync($"BookAnalysis/GetAnalysisByHash/{bookHash}");
            if (response.IsSuccessStatusCode)
            {
                
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                Console.WriteLine("11111111111111111");
                Console.WriteLine();
                Console.WriteLine("222222222222222222");
                var content = await response.Content.ReadFromJsonAsync<ServiceResponse<List<BookAnalysisModel>>>();
                return content.Content;
            }
            else
            {
                throw new Exception("Nope");
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
    }
}
