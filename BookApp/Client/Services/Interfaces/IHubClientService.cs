using BookApp.Client.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BookApp.Client.Services.Interfaces
{
    public interface IHubClientService
    {
        public HubConnection hubConnection { get; }
        public Task JoinAnalysisEditGroup(string? bookAnalysisId);
        public Task LeaveAnalysisEditGroup();
        public Task RegisterReaderHub(TextBox textbox);
    }
}
