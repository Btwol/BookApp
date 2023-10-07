using BookApp.Client.Services.Interfaces.Notes;
using BookApp.Shared.Models.ClientModels.Notes;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BookApp.Client.Services.Notes
{
    public class ParagraphNoteClientService : IParagraphNoteClientService
    {
        private readonly HttpClient Http;

        public ParagraphNoteClientService(HttpClient http)
        {
            Http = http;
        }

        public async Task<HttpResponseMessage> AddParagraphNote(ParagraphNoteModel paragraphNoteModel)
        {
            return await Http.PostAsJsonAsync($"ParagraphNote/AddParagraphNote", paragraphNoteModel);
        }
    }
}
