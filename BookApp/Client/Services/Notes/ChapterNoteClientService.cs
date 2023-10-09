﻿using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services.Notes
{
    public class ChapterNoteClientService : IChapterNoteClientService
    {
        private readonly HttpClient Http;

        public ChapterNoteClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public async Task<HttpResponseMessage> AddChapterNote(ChapterNoteModel chapterNoteModel)
        {
            return await Http.PostAsJsonAsync($"ChapterNote/AddChapterNote", chapterNoteModel);
        }

        public Task<HttpResponseMessage> AddNote(ChapterNoteModel noteModel)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EditNote(NoteModel noteModel)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EditNote(ChapterNoteModel noteModel)
        {
            throw new NotImplementedException();
        }
    }
}
