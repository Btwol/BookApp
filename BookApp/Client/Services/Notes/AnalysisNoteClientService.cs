using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Net.WebSockets;

namespace BookApp.Client.Services.Notes
{
    public class AnalysisNoteClientService : NoteClientService<AnalysisNoteModel>, IAnalysisNoteClientService
    {
        public AnalysisNoteClientService(HttpClient http, IJSRuntime jsRuntime) : base(http, jsRuntime)
        {
        }
    }
}
