using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.Enums;
using Microsoft.JSInterop;

namespace BookApp.Client.Services
{
    public class AnalysisMembershipClientService : IAnalysisMembershipClientService
    {
        private readonly HttpClient Http;

        public AnalysisMembershipClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public async Task AcceptInvite(int bookAnalysisId)
        {
            var response = await Http.PostAsync($"AnalysisMembership/AcceptInvite/{bookAnalysisId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task ChangeMemberStatus(int bookAnalysisId, int memberUserId, MemberType newMemberType)
        {
            var response = await Http.PostAsync($"AnalysisMembership/ChangeMemberStatus/{bookAnalysisId}/{memberUserId}/{newMemberType}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task DeclineInvite(int bookAnalysisId)
        {
            var response = await Http.DeleteAsync($"AnalysisMembership/DeclineInvite/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public async Task InviteUser(int bookAnalysisId, int invitedUserId)
        {
            var response = await Http.PostAsync($"AnalysisMembership/InviteUser/{bookAnalysisId}/{invitedUserId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task RemoveUser(int bookAnalysisId, int removedUserId)
        {
            var response = await Http.DeleteAsync($"AnalysisMembership/RemoveUser/{bookAnalysisId}/{removedUserId}");
            await HelperService.HandleResponse(response);
        }
    }
}
