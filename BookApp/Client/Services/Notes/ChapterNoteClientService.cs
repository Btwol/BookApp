using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using System.Net.Http.Json;

namespace BookApp.Client.Services.Notes
{
    public class ChapterNoteClientService : IChapterNoteClientService
    {
        private readonly HttpClient Http;

        public ChapterNoteClientService(HttpClient http)
        {
            Http = http;
        }

        public async Task<HttpResponseMessage> AddChapterNote(ChapterNoteModel chapterNoteModel)
        {
            return await Http.PostAsJsonAsync($"ChapterNote/AddChapterNote", chapterNoteModel);
        }
    }
}
