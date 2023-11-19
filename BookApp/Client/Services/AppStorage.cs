﻿using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Identity;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace BookApp.Client.Services
{
    public class AppStorage : IAppStorage
    {
        public const string StoredAnalysisKey = "storedBookAnalysis";
        public const string StoredBookKey = "storedBook";
        public const string CurrentUserTokenKey = "currentUserToken";
        public const string CurrentUserNameKey = "currentUsername";
        public const string CurrentUserEmailKey = "currentUserEmail";
        public const string CurrentUserIdKey = "currentUserId";

        private readonly IJSRuntime _jSRuntime;
        private readonly IBookAnalysisClientService _bookAnalysisClientService;

        public AppStorage(IJSRuntime jSRuntime, IBookAnalysisClientService bookAnalysisClientService)
        {
            _jSRuntime = jSRuntime;
            _bookAnalysisClientService = bookAnalysisClientService;
        }

        public async Task StoreBook(byte[] bookArray)
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", StoredBookKey);

            StringBuilder bytesString = new StringBuilder();
            foreach (var bt in bookArray)
            {
                bytesString.Append(bt.ToString() + " ");
            }

            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", StoredBookKey, bytesString.ToString());
        }

        public async Task<byte[]> GetStoredBook()
        {
            var book = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", StoredBookKey);
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

        public async Task DeleteBookFromStorage()
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", StoredBookKey);
        }

        public async Task<BookAnalysisDetailedModel> GetStoredBookAnalysis()
        {
            var bookAnalysisId = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", StoredAnalysisKey);
            if (bookAnalysisId is null) throw new Exception("Book analysis not loaded.");
            return await _bookAnalysisClientService.GetAnalysisById(int.Parse(bookAnalysisId));
        }

        public async Task StoreBookAnalysis(BookAnalysisDetailedModel bookAnalysis)
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", StoredAnalysisKey);
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", StoredAnalysisKey, bookAnalysis.Id);
        }

        public async Task DeleteAnalysisFromStorage()
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.removeItem", StoredAnalysisKey);

        }

        public async Task<bool> AnalysisIsStored()
        {
            var bookAnalysis = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", StoredAnalysisKey);
            if (bookAnalysis is null) return false;
            else return true;
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
    }
}
