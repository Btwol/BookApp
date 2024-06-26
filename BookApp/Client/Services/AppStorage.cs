﻿using BookApp.Client.Models;
using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Identity;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Text;

namespace BookApp.Client.Services
{
    public class AppStorage : IAppStorage
    {
        public const string StoredAnalysisIdKey = "storedBookAnalysis";
        public const string StoredBookKey = "storedBook";
        public const string StoredBookHashKey = "storedBookHash";
        public const string CurrentUserTokenKey = "currentUserToken";
        public const string CurrentUserNameKey = "currentUsername";
        public const string CurrentUserEmailKey = "currentUserEmail";
        public const string CurrentUserIdKey = "currentUserId";
        public const string UserCanEditLoadedAnalysis = "currentUserCanEditLoadedAnalysis";
        public const string ReaderPoistionKey = "readerPosition";

        private readonly IJSRuntime _jSRuntime;
        private readonly IBookAnalysisClientService _bookAnalysisClientService;

        public AppStorage(IJSRuntime jSRuntime, IBookAnalysisClientService bookAnalysisClientService)
        {
            _jSRuntime = jSRuntime;
            _bookAnalysisClientService = bookAnalysisClientService;
        }

        public async Task StoreBook(byte[] bookArray, string bookHash)
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", StoredBookKey);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", StoredBookHashKey);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", ReaderPoistionKey);

            string base64String = Convert.ToBase64String(bookArray);

            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", StoredBookKey, base64String);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", StoredBookHashKey, bookHash);
        }

        public async Task<string> GetStoredBookHash()
        {
            return await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", StoredBookHashKey);
        }

        public async Task<byte[]> GetStoredBook()
        {
            var base64String = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", StoredBookKey);

            if (string.IsNullOrEmpty(base64String))
            {
                throw new Exception("Book not loaded.");
            }

            return Convert.FromBase64String(base64String);
        }

        public async Task DeleteBookFromStorage()
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", StoredBookKey);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", StoredBookHashKey);
        }

        public async Task<BookAnalysisDetailedModel> GetStoredBookAnalysis()
        {
            var bookAnalysisId = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", StoredAnalysisIdKey);
            if (bookAnalysisId is null)
            {
                throw new Exception("Book analysis not loaded.");
            }

            return await _bookAnalysisClientService.GetAnalysisById(int.Parse(bookAnalysisId));
        }

        public async Task<string> GetStoredBookAnalysisId()
        {
            return await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", StoredAnalysisIdKey);
        }

        public async Task StoreBookAnalysis(BookAnalysisDetailedModel bookAnalysis, bool userHasEditorRights)
        {
            await DeleteAnalysisFromStorage();

            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", UserCanEditLoadedAnalysis, userHasEditorRights.ToString());
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", StoredAnalysisIdKey, bookAnalysis.Id);
        }

        public async Task DeleteAnalysisFromStorage()
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", StoredAnalysisIdKey);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", UserCanEditLoadedAnalysis);
        }

        public async Task DeleteReaderPosition()
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", ReaderPoistionKey);
        }

        public async Task<bool> AnalysisIsStored()
        {
            var bookAnalysis = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", StoredAnalysisIdKey);
            if (bookAnalysis is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task StoreUser(LoginResponse loginResponse)
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", CurrentUserTokenKey, loginResponse.Token);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", CurrentUserNameKey, loginResponse.User.Username);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", CurrentUserEmailKey, loginResponse.User.Email);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", CurrentUserIdKey, loginResponse.User.Id);
        }

        public async Task DeleteUserFromStorage()
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", CurrentUserTokenKey);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", CurrentUserNameKey);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", CurrentUserEmailKey);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", CurrentUserIdKey);
        }

        public async Task<string> GetUserToken()
        {
            return await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", CurrentUserTokenKey);
        }

        public async Task<bool> UserIsStored()
        {
            return await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", CurrentUserNameKey) != null;
        }

        public async Task<AppUserModel> GetStoredUser()
        {
            return new AppUserModel
            {
                Id = int.Parse(await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", CurrentUserIdKey)),
                Username = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", CurrentUserNameKey),
                Email = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", CurrentUserEmailKey)
            };
        }

        public async Task<bool> BookIsStored()
        {
            return await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", StoredBookHashKey) != null;
        }

        public async Task<bool> UserHasStoredAnalysisEditorialRights()
        {
            return bool.Parse(await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", UserCanEditLoadedAnalysis));
        }

        public async Task SetReaderPosition(ReaderPosition readerPosition)
        {
            string jsonString = JsonConvert.SerializeObject(readerPosition);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", ReaderPoistionKey, jsonString);
        }

        public async Task<ReaderPosition> GetLastReaderPosition()
        {
            var jsonString = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", ReaderPoistionKey);
            if(!string.IsNullOrEmpty(jsonString))
            {
                return JsonConvert.DeserializeObject<ReaderPosition>(jsonString);
            }
            else
            {
                return new ReaderPosition();
            }
        }
    }
}
