﻿using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Services.Interfaces
{
    public interface ITagServerService
    {
        public Task<ServiceResponse> GetTags(int bookAnalysisId);
        public Task<ServiceResponse> AddTag(int highlightId, int tagId);
        public Task<ServiceResponse> CreateNewTag(TagModel newTag, int bookAnalysisId);
        public Task<ServiceResponse> RemoveTag(int highlightId, int tagId);

    }
}