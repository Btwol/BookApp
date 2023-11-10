using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace BookApp.Client.Services
{
    public class BookAnalysisStorage : IBookAnalysisStorage
    {
        private readonly IJSRuntime _jSRuntime;
        public const string bookAnalysisKey = "storedBookAnalysis";
        public const string bookArrayKey = "storedBook";
        private readonly IBookAnalysisClientService _bookAnalysisClientService;

        public BookAnalysisStorage(IJSRuntime jSRuntime, IBookAnalysisClientService bookAnalysisClientService)
        {
            _jSRuntime = jSRuntime;
            _bookAnalysisClientService = bookAnalysisClientService;
        }

        public async Task SetBookArray(byte[] bookArray)
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", bookArrayKey);

            StringBuilder bytesString = new StringBuilder();
            foreach (var bt in bookArray)
            {
                bytesString.Append(bt.ToString() + " ");
            }

            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", bookArrayKey, bytesString.ToString());
        }

        public async Task<byte[]> GetLoadedBook()
        {
            var book = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", bookArrayKey);
            if (book is null) 
            {
                throw new Exception("Book not loaded."); 
            }

            var reArray = book.ToString().Split(new string[] { " " }, StringSplitOptions.None).ToList();
            reArray.Remove("");
            byte[] reByte = new byte[reArray.Count];
            for (int i = 0; i < reArray.Count - 1; i++)
            {
                reByte[i] = byte.Parse(reArray[i]);
            }

            return reByte;
        }

        public async Task<BookAnalysisDetailedModel> GetLoadedBookAnalysis()
        {
            var bookAnalysisId = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", bookAnalysisKey);
            if (bookAnalysisId is null) throw new Exception("Book analysis not loaded.");
            return await _bookAnalysisClientService.GetAnalysisById(int.Parse(bookAnalysisId));
        }

        public async Task SetBookAnalysis(BookAnalysisDetailedModel bookAnalysis)
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", bookAnalysisKey);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", bookAnalysisKey, bookAnalysis.Id);
        }

        public async Task<bool> AnalysisIsLoaded()
        {
            var bookAnalysis = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", bookAnalysisKey);
            if (bookAnalysis is null) return false;
            else return true;
        }

    }
}
