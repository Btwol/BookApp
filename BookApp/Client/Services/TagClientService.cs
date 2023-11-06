using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services
{
    public class TagClientService : ITagClientService
    {
        private readonly HttpClient Http;

        public TagClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public async Task<TagModel> CreateNewTag(TagModel newTag, int bookAnalysisId)
        {
            var response = await Http.PostAsJsonAsync<TagModel>($"Tag/CreateNewTag/{bookAnalysisId}", newTag);
            return await HelperService.HandleResponse<TagModel>(response);
        }

        public async Task DeleteTag(int tagId)
        {
            var response = await Http.DeleteAsync($"Tag/DeleteTag/{tagId}");
            await HelperService.HandleResponse(response);
        }

        public async Task EditTag(TagModel tagToEdit)
        {
            var response = await Http.PutAsJsonAsync<TagModel>($"Tag/EditTag", tagToEdit);
            await HelperService.HandleResponse(response);
        }

        public async Task<List<TagModel>> GetTags(int bookAnalysisId)
        {
            var response = await Http.GetAsync($"Tag/GetTags/{bookAnalysisId}");
            return await HelperService.HandleResponse<List<TagModel>>(response);
        }
    }
}
