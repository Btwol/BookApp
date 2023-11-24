﻿using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class ParagraphNoteServerService : NoteServerService<ParagraphNote, ParagraphNoteModel>, IParagraphNoteServerService
    {
        public ParagraphNoteServerService(INoteMapper<ParagraphNote, ParagraphNoteModel> noteMapper, IBookAnalysisRepository bookAnalysisRepository, 
            INoteRepository<ParagraphNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHubServerService hubServerService) 
            : base(noteMapper, bookAnalysisRepository, noteRepository, bookAnalysisServerService, hubServerService)
        {
        }
    }
}
