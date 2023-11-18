namespace BookApp.Server.Services
{
    public class AnalysisMembershipServerService : IAnalysisMembershipServerService
    {
        private readonly IBookAnalysisRepository _bookAnalysisRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IBaseRepository<BookAnalysisUser> _bookAnalysisUserRepository;
        private readonly IAppUserService _appUserService;

        public async Task<ServiceResponse> ChangeMemeberStatus(int bookAnalysisId, int memberUserId, MemberType newMemberType)
        {
            var analysis = await _bookAnalysisRepository.FindByConditionsFirstOrDefault(a => a.Id == bookAnalysisId);
            if (analysis is null)
            {
                return ServiceResponse.Error("Analysis does not exist.");
            }

            var modifiedUser = await _appUserRepository.FindByConditionsFirstOrDefault(u => u.Id == memberUserId);
            if (modifiedUser is null)
            {
                return ServiceResponse.Error("User does not exist.");
            }

            if (!Enum.IsDefined(typeof(MemberType), newMemberType))
            {
                return ServiceResponse.Error("Invalid member type.");
            }

            var requestorUserId = _appUserService.GetCurrentUserId();
            if (requestorUserId == memberUserId)
            {
                return ServiceResponse.Error("You cannot change your own membership type.");
            }

            var requestorMemberType = (await _bookAnalysisUserRepository.FindByConditionsFirstOrDefault(au => au.UsersId == requestorUserId && au.BookAnalysisId == bookAnalysisId)).MemberType;
            if (!(requestorMemberType == MemberType.Moderator || requestorMemberType == MemberType.Administrator))
            {
                return ServiceResponse.Error("Only the administrator or moderators can change a members type.");
            }

            var modifiedUserMembership = await _bookAnalysisUserRepository.FindByConditionsFirstOrDefault(au => au.UsersId == modifiedUser.Id && au.BookAnalysisId == bookAnalysisId);
            if (modifiedUserMembership is null) //new user invitation
            {
                modifiedUserMembership = new BookAnalysisUser { BookAnalysisId = bookAnalysisId, UsersId = modifiedUser.Id, MemberType = MemberType.Invited };
                await _bookAnalysisUserRepository.Create(modifiedUserMembership);
                return ServiceResponse.Success("User invited.");
            }

            return await ChangeMemberType(newMemberType, requestorMemberType, modifiedUserMembership);
        }

        private async Task<ServiceResponse> ChangeMemberType(MemberType newMemberType, MemberType requestorMemberType, BookAnalysisUser modifiedUserMembership)
        {
            ServiceResponse response = ServiceResponse.Error("There was an error while changing the member type");
            switch (newMemberType)
            {
                case MemberType.Administrator:
                    response = ServiceResponse.Error("There may only be one administrator");
                    break;

                case MemberType.Moderator:
                    if (requestorMemberType != MemberType.Administrator)
                    {
                        response = ServiceResponse.Error("Only the administrators can promote to moderator.");
                    }
                    else
                    {
                        modifiedUserMembership.MemberType = MemberType.Moderator;
                        await _bookAnalysisUserRepository.Edit(modifiedUserMembership);
                        response = ServiceResponse.Success("User status changed to moderator.");
                    }
                    break;
                case MemberType.Editor:
                    modifiedUserMembership.MemberType = MemberType.Editor;
                    await _bookAnalysisUserRepository.Edit(modifiedUserMembership);
                    response = ServiceResponse.Success("User status changed to editor.");
                    break;

                case MemberType.Viewer:
                    modifiedUserMembership.MemberType = MemberType.Viewer;
                    await _bookAnalysisUserRepository.Edit(modifiedUserMembership);
                    response = ServiceResponse.Success("User status changed to viewer.");
                    break;

                case MemberType.Invited:
                    response = ServiceResponse.Error("User is already invited.");
                    break;
            }

            return response;
        }


    }
}
