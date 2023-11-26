using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;

namespace BookApp.Client.Services.Notes
{
    public class HighlightNoteClientService : NoteClientService<HighlightNoteModel>, IHighlightNoteClientService
    {
        public HighlightNoteClientService(HttpClient http, IJSRuntime jsRuntime) : base(http, jsRuntime)
        {
        }
    }
}
