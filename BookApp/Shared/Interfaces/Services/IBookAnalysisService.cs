using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared.Interfaces.Services
{
    public interface IBookAnalysisService
    {
        public Task<ServiceResponse> GetBookAnalysis(int analysisId);
        public Task<ServiceResponse> CreateBookAnalysis(BookAnalysisModel newBookAnalysis);
        public Task<ServiceResponse> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysis);
        public Task<ServiceResponse> GetAnalysisByHash(string bookHash);
    }
}
