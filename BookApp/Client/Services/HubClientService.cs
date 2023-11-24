using BookApp.Client.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace BookApp.Client.Services
{
    public class HubClientService : IHubClientService
    {
        private readonly IAppStorage _appStorage;
        public HubConnection hubConnection { get; private set; }

        public HubClientService(IAppStorage appStorage, HubConnection hubConnection)
        {
            _appStorage = appStorage;
            this.hubConnection = hubConnection;
        }

        public async Task JoinAnalysisEditGroup(string? bookAnalysisId)
        {
            if (hubConnection.State != HubConnectionState.Connected)
            {
                await hubConnection.StartAsync();
            }
            bookAnalysisId = (string.IsNullOrEmpty(bookAnalysisId)) ? await _appStorage.GetStoredBookAnalysisId() : bookAnalysisId;
            await hubConnection.SendAsync("JoinAnalysisEditGroup", bookAnalysisId); //sends message back to server hub
        }

        public async Task LeaveAnalysisEditGroup()
        {
            if(hubConnection.State != HubConnectionState.Disconnected)
            {
                var bookAnalysisId = await _appStorage.GetStoredBookAnalysisId();
                if(!string.IsNullOrEmpty(bookAnalysisId))
                {
                    await hubConnection.SendAsync("LeaveAnalysisEditGroup", bookAnalysisId); //sends message back to server hub
                }
            }
        }
    }
}
