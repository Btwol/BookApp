using Microsoft.AspNetCore.SignalR;

namespace BookApp.Server.Hubs
{
    public class BookAnalysisHub : Hub
    {
        private const string EditAnalysisRoomPrefix = "analysisEditRoom_";

        public async Task BookAnalysisSummaryUpdated(BookAnalysisSummaryModel updatedTask)
        {
            await Clients.All.SendAsync("BookAnalysisSummaryUpdated", updatedTask);
        }

        public static string GetAnalysisEditRoomName(string bookAnalysisId)
        {
            return EditAnalysisRoomPrefix + bookAnalysisId;
        }

        public static string GetAnalysisEditRoomName(int bookAnalysisId)
        {
            return EditAnalysisRoomPrefix + bookAnalysisId;
        }

        public async Task JoinAnalysisEditGroup(string bookAnalysisId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GetAnalysisEditRoomName(bookAnalysisId));
        }

        public async Task LeaveAnalysisEditGroup(string bookAnalysisId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetAnalysisEditRoomName(bookAnalysisId));
        }
    }
}
