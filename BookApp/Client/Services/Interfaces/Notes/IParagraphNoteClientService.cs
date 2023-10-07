using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Client.Services.Interfaces.Notes
{
    public interface IParagraphNoteClientService
    {
        public Task<HttpResponseMessage> AddParagraphNote(ParagraphNoteModel paragraphNoteModel);
    }
}
