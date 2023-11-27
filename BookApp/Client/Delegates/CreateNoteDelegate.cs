using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Client.Delegates
{
    public delegate INoteClientModel CreateNoteDelegate(INoteClientModel note);
}
