using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services.Notes
{
    public abstract class NoteClientService<T> : INoteClientService<T>  where T : NoteModel
    {
        protected readonly HttpClient _http;
        protected readonly string noteType;
        protected readonly IJSRuntime _jsRuntime;

        public NoteClientService(HttpClient http, IJSRuntime jSRuntime)
        {
            noteType = typeof(T).Name.Replace("Model", string.Empty);
            this._http = http;
            this._jsRuntime = jSRuntime;
        }

        public virtual async Task<T> AddNote(T noteModel)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PostAsJsonAsync($"{noteType}/Add{noteType}", noteModel);
            return await HelperService.HandleResponse<T>(response);
        }

        public virtual async Task DeleteNote(int noteId, int bookAnalysisId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.DeleteAsync($"{noteType}/Delete{noteType}/{noteId}/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public virtual async Task<T> EditNote(T noteModel)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PutAsJsonAsync($"{noteType}/Edit{noteType}", noteModel);
            return await HelperService.HandleResponse<T>(response);
        }

        public virtual async Task AddTag(int noteId, int tagId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PostAsync($"{noteType}/AddTag/{noteId}/{tagId}", null);
            await HelperService.HandleResponse(response);
        }

        public virtual async Task RemoveTag(int noteId, int tagId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.DeleteAsync($"{noteType}/RemoveTag/{noteId}/{tagId}");
            await HelperService.HandleResponse(response);
        }
    }
}
