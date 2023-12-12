namespace BookApp.Server.Services
{
    public class AnalysisUpdaterServerService : IAnalysisUpdaterServerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AnalysisUpdaterServerService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> UpdateAnalysisModel(AnalysisVersionModel clientAnalysisVersion)
        {
            UpdatedBookAnalysisModel updateAnalysisModel = new();
            AnalysisVersion databaseAnalysisVersion = await _context.AnalysisVersions.FirstOrDefaultAsync(a => a.BookAnalysis.Id == clientAnalysisVersion.BookAnalysisId);

            if(databaseAnalysisVersion is null)
            {
                return ServiceResponse.Error("Analysis not found.");
            }

            var updatedBookAnalysis = await QueryWithIncludes(_context.Set<BookAnalysis>(), clientAnalysisVersion, databaseAnalysisVersion)
                .FirstOrDefaultAsync(a => a.Id == clientAnalysisVersion.BookAnalysisId);

            updateAnalysisModel.AnalysisVersion = _mapper.Map<AnalysisVersionModel>(databaseAnalysisVersion);

            updateAnalysisModel.UpToDate = clientAnalysisVersion.Equals(updateAnalysisModel.AnalysisVersion);

            if (updatedBookAnalysis.AnalysisNotes is not null)
            {
                MapModels(updateAnalysisModel.AnalysisNotes, updatedBookAnalysis.AnalysisNotes);
            }
            if (updatedBookAnalysis.ChapterNotes is not null)
            {
                MapModels(updateAnalysisModel.ChapterNotes, updatedBookAnalysis.ChapterNotes);
            }
            if (updatedBookAnalysis.ParagraphNotes is not null)
            {
                MapModels(updateAnalysisModel.ParagraphNotes, updatedBookAnalysis.ParagraphNotes);
            }
            if (updatedBookAnalysis.Highlights is not null)
            {
                MapModels(updateAnalysisModel.Highlights, updatedBookAnalysis.Highlights);
                foreach(var highlight in updatedBookAnalysis.Highlights)
                {
                    if(highlight.HighlightNotes is not null)
                    {
                        MapModels(updateAnalysisModel.HighlightNotes, highlight.HighlightNotes);
                    }
                }
            }
            if (updatedBookAnalysis.Tags is not null)
            {
                MapModels(updateAnalysisModel.Tags, updatedBookAnalysis.Tags);
            }
            
            return ServiceResponse<UpdatedBookAnalysisModel>.Success(updateAnalysisModel);
        }

        private void MapModels<D, C>(List<C>? clientModels, List<D>? databaseModels) where D : IDbModel where C : IClientModel
        {
            clientModels = new();
            foreach (var analysisNote in databaseModels)
            {
                clientModels.Add(_mapper.Map<C>(analysisNote));
            }
        }

        public IQueryable<BookAnalysis> QueryWithIncludes(DbSet<BookAnalysis> querry, AnalysisVersionModel analysisVersionModel, AnalysisVersion analysisVersion)
        {
            if (analysisVersionModel.AnalysisNotesVersion != analysisVersion.AnalysisNotesVersion)
                querry.Include(t => t.AnalysisNotes);
            if (analysisVersionModel.ParagraphNotesVersion != analysisVersion.ParagraphNotesVersion)
                querry.Include(t => t.ParagraphNotes);
            if (analysisVersionModel.ChapterNotesVersion != analysisVersion.ChapterNotesVersion)
                querry.Include(t => t.ChapterNotes);
            if (analysisVersionModel.HighlightNotesVersion != analysisVersion.HighlightNotesVersion)
                querry.Include(t => t.Highlights).ThenInclude(h => h.HighlightNotes);
            else if (analysisVersionModel.HighlightVersion != analysisVersion.HighlightVersion)
                querry.Include(t => t.Highlights);
            if (analysisVersionModel.TagsVersion != analysisVersion.TagsVersion)
                querry.Include(t => t.Tags);
            return querry;
        }
    }
}
