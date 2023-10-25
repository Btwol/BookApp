using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services.Notes
{
    public class ParagraphNoteClientService : NoteClientService<ParagraphNoteModel>, IParagraphNoteClientService
    {
        public ParagraphNoteClientService(HttpClient http, IJSRuntime jsRuntime) : base(http, jsRuntime)
        {
        }
    }
}
