﻿namespace BookApp.Server.Services.Notes
{
    public class AnalysisNoteServerService : NoteServerService<AnalysisNote, AnalysisNoteModel>, IAnalysisNoteServerService
    {
        public AnalysisNoteServerService(IAnalysisNoteMapper noteMapper, IBookAnalysisRepository bookAnalysisRepository,
            IAnalysisNoteRepository noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHubServerService hubServerService,
            ITagRepository tagRepository)
            : base(noteMapper, bookAnalysisRepository, noteRepository, bookAnalysisServerService, hubServerService, tagRepository)
        {
        }
    }
}
