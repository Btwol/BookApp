﻿namespace BookApp.Server.Services.Notes
{
    public class HighlightNoteServerService : NoteServerService<HighlightNote, HighlightNoteModel>, IHighlightNoteServerService
    {
        private readonly IHighlightRepository _highlightRepository;

        public HighlightNoteServerService(IHighlightNoteMapper noteMapper, IBookAnalysisRepository bookAnalysisRepository,
            IHighlightNoteRepository noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHubServerService hubServerService,
            IHighlightRepository highlightRepository, ITagRepository tagRepository)
            : base(noteMapper, bookAnalysisRepository, noteRepository, bookAnalysisServerService, hubServerService, tagRepository)
        {
            _highlightRepository = highlightRepository;
        }

        protected override async Task<ServiceResponse> ValidateNoteRequest<T>(int bookAnalysisId, T noteModel)
        {
            if (noteModel is HighlightNoteModel highlightNoteModel)
            {
                if (!await _highlightRepository.CheckIfExists(h => h.Id == highlightNoteModel.HighlightId))
                {
                    return ServiceResponse.Error("Highlight does not exist.");
                }
            }

            return await base.ValidateNoteRequest(bookAnalysisId, noteModel);
        }

        protected override async Task<ServiceResponse<HighlightNoteModel>> SaveNote(HighlightNoteModel noteModel)
        {
            var mappedNote = await _noteMapper.MapToDbModel(noteModel);
            await IncludeTags(mappedNote);
            var savedNoteId = (await _noteRepository.Create(mappedNote)).Id;
            var createdNote = await _noteMapper.MapToClientModel(await _noteRepository.FindByConditionsFirstOrDefault(n => n.Id == savedNoteId));

            return ServiceResponse<HighlightNoteModel>.Success(createdNote, "Note added.");
        }
    }
}
