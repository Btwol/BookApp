namespace BookApp.Server.Services
{
    public class AnalysisMembershipServerService : IAnalysisMembershipServerService
    {
        private readonly IBookAnalysisRepository _bookAnalysisRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IBookAnalysisUserRepository _bookAnalysisUserRepository;
        private readonly IAppUserService _appUserService;

        public AnalysisMembershipServerService(IBookAnalysisRepository bookAnalysisRepository, IAppUserRepository appUserRepository, 
            IBookAnalysisUserRepository bookAnalysisUserRepository, IAppUserService appUserService)
        {
            _bookAnalysisRepository = bookAnalysisRepository;
            _appUserRepository = appUserRepository;
            _bookAnalysisUserRepository = bookAnalysisUserRepository;
            _appUserService = appUserService;
        }

        public async Task<ServiceResponse> ChangeMemberStatus(int bookAnalysisId, int memberUserId, MemberType newMemberType)
        {
            var modifiedUserMembership = await _bookAnalysisUserRepository.FindByConditionsFirstOrDefault(au => au.UsersId == memberUserId && au.BookAnalysisId == bookAnalysisId);
            if(modifiedUserMembership is null)
            {
                return ServiceResponse.Error("User not a member of analysis.");
            }

            if (!Enum.IsDefined(typeof(MemberType), newMemberType) || newMemberType == MemberType.Invited)
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

            return await ChangeMemberType(newMemberType, requestorMemberType, modifiedUserMembership);
        }

        private async Task<ServiceResponse> ChangeMemberType(MemberType newMemberType, MemberType requestorMemberType, BookAnalysisUser modifiedUserMembership)
        {
            switch (newMemberType)
            {
                case MemberType.Administrator:
                    return ServiceResponse.Error("There may only be one administrator");

                case MemberType.Moderator:
                    if (requestorMemberType != MemberType.Administrator)
                    {
                        return ServiceResponse.Error("Only the administrators can promote to moderator.");
                    }
                    else
                    {
                        modifiedUserMembership.MemberType = MemberType.Moderator;
                        await _bookAnalysisUserRepository.Edit(modifiedUserMembership);
                        return ServiceResponse.Success("User status changed to moderator.");
                    }

                case MemberType.Editor:
                    modifiedUserMembership.MemberType = MemberType.Editor;
                    await _bookAnalysisUserRepository.Edit(modifiedUserMembership);
                    return ServiceResponse.Success("User status changed to editor.");

                case MemberType.Viewer:
                    modifiedUserMembership.MemberType = MemberType.Viewer;
                    await _bookAnalysisUserRepository.Edit(modifiedUserMembership);
                    return ServiceResponse.Success("User status changed to viewer.");
            }

            return ServiceResponse.Error("There was an error while changing the member type");
        }

        public async Task<ServiceResponse> InviteUser(int bookAnalysisId, int invitedUserId)
        {
            if(await _bookAnalysisUserRepository.CheckIfExists(au => au.BookAnalysisId == bookAnalysisId && au.UsersId == invitedUserId))
            {
                return ServiceResponse.Error("User already invited to analysis.");
            }

            var analysis = await _bookAnalysisRepository.FindByConditionsFirstOrDefault(a => a.Id == bookAnalysisId);
            if (analysis is null)
            {
                return ServiceResponse.Error("Analysis does not exist.");
            }

            var invitedUser = await _appUserRepository.FindByConditionsFirstOrDefault(u => u.Id == invitedUserId);
            if (invitedUser is null)
            {
                return ServiceResponse.Error("User does not exist.");
            }

            var requestorUserId = _appUserService.GetCurrentUserId();
            if (requestorUserId == invitedUserId)
            {
                return ServiceResponse.Error("You cannot change your own membership type.");
            }

            var requestorMemberType = (await _bookAnalysisUserRepository.FindByConditionsFirstOrDefault(au => au.UsersId == requestorUserId && au.BookAnalysisId == bookAnalysisId)).MemberType;
            if (!(requestorMemberType == MemberType.Moderator || requestorMemberType == MemberType.Administrator))
            {
                return ServiceResponse.Error("Only the administrator or moderators can send invitations.");
            }

            var newMembership = new BookAnalysisUser { BookAnalysisId = bookAnalysisId, UsersId = invitedUserId, MemberType = MemberType.Invited };
            await _bookAnalysisUserRepository.Create(newMembership);

            return ServiceResponse.Success("User invited.");
        }

        public async Task<ServiceResponse> RemoveUser(int bookAnalysisId, int removedUserId)
        {
            var modifiedUserMembership = await _bookAnalysisUserRepository.FindByConditionsFirstOrDefault(au => au.UsersId == removedUserId && au.BookAnalysisId == bookAnalysisId);
            if (modifiedUserMembership is null)
            {
                return ServiceResponse.Error("User not a member of analysis.");
            }

            var requestorUserId = _appUserService.GetCurrentUserId();
            var requestorMemberType = (await _bookAnalysisUserRepository.FindByConditionsFirstOrDefault(au => au.UsersId == requestorUserId && au.BookAnalysisId == bookAnalysisId)).MemberType;

            if (requestorUserId == removedUserId)
            {
                if(requestorMemberType == MemberType.Administrator)
                {
                    return ServiceResponse.Error("You cannot leave the analysis if you are the administrator.");
                }
                else
                {
                    await _bookAnalysisUserRepository.Delete(modifiedUserMembership);
                    return ServiceResponse.Success("Analysis left.");
                }
            }

            if (!(requestorMemberType == MemberType.Moderator || requestorMemberType == MemberType.Administrator))
            {
                return ServiceResponse.Error("Only the administrator or moderators can remove users.");
            }

            switch(modifiedUserMembership.MemberType)
            {
                case MemberType.Administrator:
                    return ServiceResponse.Error("You cannot remove the administrator.");

                case MemberType.Moderator:
                    if(requestorMemberType == MemberType.Administrator)
                    {
                        await _bookAnalysisUserRepository.Delete(modifiedUserMembership);
                        return ServiceResponse.Success("User removed.");
                    }
                    else
                    {
                        return ServiceResponse.Error("Only the administrator can remove moderators.");
                    }

                default:
                    await _bookAnalysisUserRepository.Delete(modifiedUserMembership);
                    return ServiceResponse.Success("User removed.");
            }
        }
    }
}
