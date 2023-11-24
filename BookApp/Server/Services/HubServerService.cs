using BookApp.Server.Hubs;
using BookApp.Server.Models;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.ClientModels.Notes;
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

        public async Task AnalysisMemberAdded(int bookAnalysisId, int userId)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("AnalysisMemberAdded", userId);
        }

        public async Task AnalysisMemberModified(int bookAnalysisId, int userId, MemberType memberType)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("AnalysisMemberModified", userId, memberType);
        }

        public async Task AnalysisMemberRemoved(int bookAnalysisId, int userId)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("AnalysisMemberRemoved", userId);
        }

        public async Task AnalysisSummaryUpdated(BookAnalysisSummaryModel bookAnalysisSummaryModel)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisSummaryModel.Id))
                .SendAsync("AnalysisSummaryUpdated", bookAnalysisSummaryModel);
        }

        public async Task HighlightAdded(HighlightModel highlight)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(highlight.BookAnalysisId))
                .SendAsync("HighlightAdded", highlight);
        }

        public async Task HighlightRemoved(int bookAnalysisId, int highlightId)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("HighlightRemoved", highlightId);
        }

        public async Task HighlightUpdated(HighlightModel highlight)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(highlight.BookAnalysisId))
                .SendAsync("HighlightUpdated", highlight);
        }

        public async Task NoteCreated(int bookAnalysisId, INoteClientModel noteModel)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("NoteCreated", noteModel);
        }

        public async Task NoteDeleted(int bookAnalysisId, int noteId)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("NoteDeleted", noteId);
        }

        public async Task NoteUpdated(int bookAnalysisId, INoteClientModel noteModel)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("NoteUpdated", noteModel);
        }

        public async Task TagAdded(int bookAnalysisId, int tagId, int taggedId)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("TagAdded", tagId, taggedId);
        }

        public async Task TagCreated(int bookAnalysisId, TagModel tagModel)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("TagCreated", tagModel);
        }

        public async Task TagDeleted(int bookAnalysisId, int tagId)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("TagDeleted", tagId);
        }

        public async Task TagRemoved(int bookAnalysisId, int tagId, int taggedId)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("TagRemoved", tagId, taggedId);
        }

        public async Task TagUpdated(int bookAnalysisId, TagModel tagModel)
        {
            await _hubContext.Clients.Group(BookAnalysisHub.GetAnalysisEditRoomName(bookAnalysisId))
                .SendAsync("TagUpdated", tagModel);
        }
    }
}
