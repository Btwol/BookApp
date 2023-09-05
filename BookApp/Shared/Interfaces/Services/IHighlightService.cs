using BookApp.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared.Interfaces.Services
{
    public interface IHighlightService
    {
        public Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight);
        public Task<ServiceResponse> RetrieveHighlights(int bookAnalysisId);
    }
}
