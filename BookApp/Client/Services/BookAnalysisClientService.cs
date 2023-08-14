using BookApp.Client.Services.Interfaces;
using BookApp.Shared;
using BookApp.Shared.Data;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BookApp.Client.Services
{
    public class BookAnalysisClientService : IBookAnalysisClientService
    {
        private readonly HttpClient Http;
        private static List<BookAnalysis> bookAnalysesStorage = new();

        public BookAnalysisClientService(HttpClient http)
        {
            Http = http;

            //bookAnalysesStorage.Add(new BookAnalysis
            //{
            //    AnalysisTitle = "Bob Book analysis v1",
            //    BookHash = "22836997B8F82310BA982D76BF1A6CED395FB1226E7BCC26DBF2FE72395E02C1",
            //});
        }

        public async Task<BookAnalysis> CreateBookAnalysis(BookAnalysis newBookAnalysis)
        {
            bookAnalysesStorage.Add(newBookAnalysis);
            return newBookAnalysis;
        }

        public async Task<List<BookAnalysis>> GetAnalysisByHash(string bookHash)
        {
            //replace with server ask

            return bookAnalysesStorage.Where(b => b.BookHash == bookHash).ToList();
        }

        public async Task<BookAnalysis> GetBookAnalysis(Guid analysisId)
        {
            return null;
            //await Http.GetFromJsonAsync<BookAnalysis>("GetBookAnalysis");
        }

        public async Task<BookAnalysis> UpdateBookAnalysis(BookAnalysis updatedBookAnalysis)
        {
            var bookAnalysis = bookAnalysesStorage.FirstOrDefault(b => b.AnalysisId == updatedBookAnalysis.AnalysisId);
            bookAnalysesStorage.Remove(bookAnalysis);
            bookAnalysesStorage.Add(updatedBookAnalysis);
            return updatedBookAnalysis;
        }
    }
}
