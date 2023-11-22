using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services
{
    public class TagClientService : ITagClientService
    {
        private readonly HttpClient _http;
        protected readonly IJSRuntime _jsRuntime;

        public TagClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            _http = http;
            _jsRuntime = jsRuntime;
        }

        public async Task<TagModel> CreateNewTag(TagModel newTag, int bookAnalysisId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PostAsJsonAsync<TagModel>($"Tag/CreateNewTag/{bookAnalysisId}", newTag);
            return await HelperService.HandleResponse<TagModel>(response);
        }

        public async Task DeleteTag(int tagId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.DeleteAsync($"Tag/DeleteTag/{tagId}");
            await HelperService.HandleResponse(response);
        }

        public async Task EditTag(TagModel tagToEdit)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PutAsJsonAsync<TagModel>($"Tag/EditTag", tagToEdit);
            await HelperService.HandleResponse(response);
        }

        public async Task<List<TagModel>> GetTags(int bookAnalysisId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.GetAsync($"Tag/GetTags/{bookAnalysisId}");
            return await HelperService.HandleResponse<List<TagModel>>(response);
        }
    }
}
