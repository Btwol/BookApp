﻿using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Client.Services.Interfaces.Notes
{
    public interface IChapterNoteClientService
    {
        public Task<HttpResponseMessage> AddChapterNote(ChapterNoteModel chapterNoteModel);
    }
}