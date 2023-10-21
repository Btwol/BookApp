﻿using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

namespace BookApp.Client.Services.Interfaces
{
    public interface IHighlightClientService : ITagManagerClientService
    {
        public Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight);
        public Task<HttpResponseMessage> AddHighlight(HighlightModel newHighlight);
        public Task<HttpResponseMessage> DeleteHighlight(int highlightId);
    }
}
