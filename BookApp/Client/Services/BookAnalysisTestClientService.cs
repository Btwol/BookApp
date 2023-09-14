using BookApp.Client.Services.Interfaces;
using BookApp.Shared;
using BookApp.Shared.Models.ClientModels;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BookApp.Client.Services
{
    public class BookAnalysisTestClientService //: IBookAnalysisClientService
    {
        private readonly HttpClient Http;
        private static List<BookAnalysisModel> bookAnalysesStorage = new();

        public BookAnalysisTestClientService(HttpClient http)
        {
            Http = http;

            //bookAnalysesStorage.Add(new BookAnalysis
            //{
            //    AnalysisTitle = "Bob Book analysis v1",
            //    BookHash = "22836997B8F82310BA982D76BF1A6CED395FB1226E7BCC26DBF2FE72395E02C1",
            //});
        }

        //public async Task<ServiceResponse> CreateBookAnalysis(BookAnalysisModel newBookAnalysis)
        //{
        //    bookAnalysesStorage.Add(newBookAnalysis);
        //    return newBookAnalysis;
        //}

        //public async Task<ServiceResponse> GetAnalysisByHash(string bookHash)
        //{
        //    //replace with server ask

        //    return bookAnalysesStorage.Where(b => b.BookHash == bookHash).ToList();
        //}

        //public async Task<ServiceResponse> GetBookAnalysis(int analysisId)
        //{
        //    return null;
        //    //await Http.GetFromJsonAsync<BookAnalysis>("GetBookAnalysis");
        //}

        //public async Task<ServiceResponse> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysis)
        //{
        //    var bookAnalysis = bookAnalysesStorage.FirstOrDefault(b => b.Id == updatedBookAnalysis.Id);
        //    bookAnalysesStorage.Remove(bookAnalysis);
        //    bookAnalysesStorage.Add(updatedBookAnalysis);
        //    return updatedBookAnalysis;
        //}
    }
}
