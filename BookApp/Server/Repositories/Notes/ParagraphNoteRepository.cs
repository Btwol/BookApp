﻿using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Repositories.Notes
{
    public class ParagraphNoteRepository : NoteRepository<ParagraphNote>, IParagraphNoteRepository
    {
        public ParagraphNoteRepository(DataContext context) : base(context) { }
    }
}
