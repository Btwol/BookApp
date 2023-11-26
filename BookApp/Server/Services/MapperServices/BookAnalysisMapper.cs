﻿using BookApp.Shared.Enums;

namespace BookApp.Server.Services.MapperServices
{
    public class BookAnalysisMapper : MapperService<BookAnalysis, BookAnalysisSummaryModel>, IBookAnalysisMapperService
    {
        private readonly IBookAnalysisUserRepository _bookAnalysisUserRepository;
        private readonly IAppUserMapperService _appUserMapperService;

        public BookAnalysisMapper(IMapper mapper, IBookAnalysisUserRepository bookAnalysisUserRepository, IAppUserMapperService appUserMapperService) : base(mapper)
        {
            _bookAnalysisUserRepository = bookAnalysisUserRepository;
            _appUserMapperService = appUserMapperService;
        }

        public void MapEditBookAnalysis(BookAnalysis analysistoUpdate, BookAnalysisSummaryModel updatedBookAnalysisModel)
        {
            analysistoUpdate.AnalysisTitle = updatedBookAnalysisModel.AnalysisTitle;
        }

        public async Task<BookAnalysisDetailedModel> MapToDetailedModel(BookAnalysis bookAnalysis)
        {
            var mappedAnalysis = _mapper.Map<BookAnalysisDetailedModel>(bookAnalysis);
            await IncludeMembers(bookAnalysis, mappedAnalysis);
            return mappedAnalysis;
        }

        public override async Task<BookAnalysisSummaryModel> MapToClientModel(BookAnalysis dbModel)
        {
            var mappedAnalysis = await base.MapToClientModel(dbModel);
            await IncludeMembers(dbModel, mappedAnalysis);
            return mappedAnalysis;
        }

        private async Task IncludeMembers(BookAnalysis bookAnalysis, BookAnalysisSummaryModel mapped)
        {
            foreach (var member in bookAnalysis.Users)
            {
                mapped.Members.Add(new KeyValuePair<Shared.Models.Identity.AppUserModel, MemberType>
                (
                    await _appUserMapperService.MapGetApiUserResponseDto(member),
                    (await _bookAnalysisUserRepository.FindByConditionsFirstOrDefault(au => au.UsersId == member.Id && au.BookAnalysisId == bookAnalysis.Id)).MemberType)
                );
            }
        }
    }
}
