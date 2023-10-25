using BookApp.Client.Services.Interfaces.Notes;
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

        public async Task<ChapterNoteModel> AddNote(ChapterNoteModel chapterNoteModel)
        {
            var response = await Http.PostAsJsonAsync($"ChapterNote/AddChapterNote", chapterNoteModel);
            return await HelperService.HandleResponse<ChapterNoteModel>(response);
        }

        public async Task DeleteNote(int noteId, int bookAnalysisId)
        {
            var response = await Http.DeleteAsync($"ChapterNote/DeleteChapterNote/{noteId}/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public async Task<ChapterNoteModel> EditNote(ChapterNoteModel chapterNoteModel)
        {
            var response = await Http.PutAsJsonAsync($"ChapterNote/EditChapterNote", chapterNoteModel);
            return await HelperService.HandleResponse<ChapterNoteModel>(response);
        }

        public async Task AddTag(int chapterNoteId, int tagId)
        {
            var response = await Http.PostAsync($"ChapterNote/AddTag/{chapterNoteId}/{tagId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task RemoveTag(int chapterNoteId, int tagId)
        {
            var response = await Http.DeleteAsync($"ChapterNote/RemoveTag/{chapterNoteId}/{tagId}");
            await HelperService.HandleResponse(response);
        }
    }

}
