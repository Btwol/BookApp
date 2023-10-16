using BookApp.Server.Models;
using BookApp.Server.Repositories.Interfaces.Notes;
using BookApp.Server.Services.Interfaces;

namespace BookApp.Server.Services.Notes
{
    public class AnalysisNoteService : NoteService<AnalysisNote, AnalysisNoteModel>, IAnalysisNoteService
    {
        public AnalysisNoteService(IAnalysisNoteMapperService analysisNoteMapperService, IBookAnalysisRepository bookAnalysisRepository, 
            INoteRepository<AnalysisNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService) 
            : base(analysisNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
