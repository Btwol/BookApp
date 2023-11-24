using Microsoft.AspNetCore.SignalR;

namespace BookApp.Server.Hubs
{
    public class BookAnalysisHub : Hub
    {
        private const string EditAnalysisRoomPrefix = "analysisEditRoom_";
        public async Task BookAnalysisSummaryUpdated(BookAnalysisSummaryModel updatedTask)  //recievs messages from client
        {
            // Update the task in the database (Entity Framework) or any other storage.
            // For simplicity, we'll omit the database update here.

            // Notify all connected clients about the updated task.
            await Clients.All.SendAsync("BookAnalysisSummaryUpdated", updatedTask);
        }

        public static string GetAnalysisEditRoomName(int bookAnalysisId)
        {
            return EditAnalysisRoomPrefix + bookAnalysisId;
        }

        public async Task JoinAnalysisEditGroup(int bookAnalysisId)  //recievs messages from client
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GetAnalysisEditRoomName(bookAnalysisId));
        }

        public async Task LeaveAnalysisEditGroup(int bookAnalysisId)  //recievs messages from client
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetAnalysisEditRoomName(bookAnalysisId));
        }
    }
}
