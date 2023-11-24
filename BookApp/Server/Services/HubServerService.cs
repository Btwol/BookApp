using BookApp.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BookApp.Server.Services
{
    public class HubServerService : IHubServerService
    {
        private readonly IHubContext<BookAnalysisHub> _hubContext;

        public HubServerService(IHubContext<BookAnalysisHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AnalysisSummaryUpdated(BookAnalysisSummaryModel bookAnalysisSummaryModel)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisSummaryModel.Id))
                .SendAsync("BookAnalysisSummaryUpdated", bookAnalysisSummaryModel);
        }
    }
}
