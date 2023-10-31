using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services.Notes
{
    public abstract class NoteClientService<T> : INoteClientService<T>  where T : NoteModel
    {
        protected readonly HttpClient Http;
        protected readonly string NoteType;

        public NoteClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            NoteType = typeof(T).Name.Replace("Model", string.Empty);
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public virtual async Task<T> AddNote(T noteModel)
        {
            var response = await Http.PostAsJsonAsync($"{NoteType}/Add{NoteType}", noteModel);
            return await HelperService.HandleResponse<T>(response);
        }

        public virtual async Task DeleteNote(int noteId, int bookAnalysisId)
        {
            var response = await Http.DeleteAsync($"{NoteType}/Delete{NoteType}/{noteId}/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public virtual async Task<T> EditNote(T noteModel)
        {
            var response = await Http.PutAsJsonAsync($"{NoteType}/Edit{NoteType}", noteModel);
            return await HelperService.HandleResponse<T>(response);
        }

        public virtual async Task AddTag(int noteId, int tagId)
        {
            var response = await Http.PostAsync($"{NoteType}/AddTag/{noteId}/{tagId}", null);
            await HelperService.HandleResponse(response);
        }

        public virtual async Task RemoveTag(int noteId, int tagId)
        {
            var response = await Http.DeleteAsync($"{NoteType}/RemoveTag/{noteId}/{tagId}");
            await HelperService.HandleResponse(response);
        }
    }
}
