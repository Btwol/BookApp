using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Enums;
using Microsoft.JSInterop;

namespace BookApp.Client.Services
{
    public class AnalysisMembershipClientService : IAnalysisMembershipClientService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;

        public AnalysisMembershipClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            _http = http;
            this._jsRuntime = jsRuntime;
        }

        public async Task AcceptInvite(int bookAnalysisId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PostAsync($"AnalysisMembership/AcceptInvite/{bookAnalysisId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task ChangeMemberStatus(int bookAnalysisId, int memberUserId, MemberType newMemberType)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PostAsync($"AnalysisMembership/ChangeMemberStatus/{bookAnalysisId}/{memberUserId}/{newMemberType}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task DeclineInvite(int bookAnalysisId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.DeleteAsync($"AnalysisMembership/DeclineInvite/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public async Task InviteUser(int bookAnalysisId, int invitedUserId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PostAsync($"AnalysisMembership/InviteUser/{bookAnalysisId}/{invitedUserId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task RemoveUser(int bookAnalysisId, int removedUserId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.DeleteAsync($"AnalysisMembership/RemoveUser/{bookAnalysisId}/{removedUserId}");
            await HelperService.HandleResponse(response);
        }
    }
}
