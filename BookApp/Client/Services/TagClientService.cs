using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Enums;
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

        public async Task<HttpResponseMessage> AddTag(int tagId, int highlightId, TaggedType taggedType)
        {
            return await Http.PostAsync($"Tag/AddTag/{highlightId}/{tagId}/{taggedType}", null);
        }

        public async Task<HttpResponseMessage> CreateNewTag(TagModel newTag, int bookAnalysisId)
        {
            return await Http.PostAsJsonAsync<TagModel>($"Tag/CreateNewTag/{bookAnalysisId}", newTag);
        }

        public async Task<HttpResponseMessage> GetTags(int bookAnalysisId)
        {
            return await Http.GetAsync($"Tag/GetTags/{bookAnalysisId}");
        }

        public async Task<HttpResponseMessage> RemoveTag(int tagId, int highlightId)
        {
            return await Http.DeleteAsync($"Tag/RemoveTag/{highlightId}/{tagId}");
        }
    }
}
