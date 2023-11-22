using Microsoft.AspNetCore.SignalR;

namespace BookApp.Server.Hubs
{
    public class BookAnalysisHub : Hub
    {
        public async Task BookAnalysisSummaryUpdated(BookAnalysisSummaryModel updatedTask)
        {
            // Update the task in the database (Entity Framework) or any other storage.
            // For simplicity, we'll omit the database update here.

            // Notify all connected clients about the updated task.
            await Clients.All.SendAsync("BookAnalysisSummaryUpdated", updatedTask);
        }
    }
}
