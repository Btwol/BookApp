using BookApp.Shared.Enums;
using BookApp.Shared.Models.ClientModels;

namespace BookApp.Client.Services.Interfaces
{
    public interface ITagClientService
    {
        public Task<TagModel> CreateNewTag(TagModel newTag, int bookAnalysisId);
        public Task EditTag(TagModel tagToEdit);
        public Task DeleteTag(int tagId);
    }
}
