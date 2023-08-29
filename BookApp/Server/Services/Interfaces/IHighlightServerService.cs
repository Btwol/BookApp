﻿using BookApp.Shared.Data;
using BookApp.Shared.Interfaces.Services;

namespace BookApp.Server.Services.Interfaces
{
    public interface IHighlightServerService
    {
        public Task<ServiceResponse> AddHighlight(HighlightModel newHighlight);
    }
}
