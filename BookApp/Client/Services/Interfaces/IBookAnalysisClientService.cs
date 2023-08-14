﻿using BookApp.Shared.Data;

namespace BookApp.Client.Services.Interfaces
{
    public interface IBookAnalysisClientService
    {
        public Task<BookAnalysis> GetBookAnalysis(Guid analysisId);
        public Task<BookAnalysis> CreateBookAnalysis(BookAnalysis newBookAnalysis);
        public Task<BookAnalysis> UpdateBookAnalysis(BookAnalysis updatedBookAnalysis);
        public Task<List<BookAnalysis>> GetAnalysisByHash(string bookHash);
    }
}
