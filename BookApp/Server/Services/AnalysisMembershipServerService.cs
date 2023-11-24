namespace BookApp.Server.Services
{
    public class AnalysisMembershipServerService : IAnalysisMembershipServerService
    {
        private readonly IBookAnalysisRepository _bookAnalysisRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IBookAnalysisUserRepository _bookAnalysisUserRepository;
        private readonly IAppUserService _appUserService;
        private readonly IHubServerService _hubServerService;

        public AnalysisMembershipServerService(IBookAnalysisRepository bookAnalysisRepository, IAppUserRepository appUserRepository,
            IBookAnalysisUserRepository bookAnalysisUserRepository, IAppUserService appUserService, IHubServerService hubServerService)
        {
            _bookAnalysisRepository = bookAnalysisRepository;
            _appUserRepository = appUserRepository;
            _bookAnalysisUserRepository = bookAnalysisUserRepository;
            _appUserService = appUserService;
            _hubServerService = hubServerService;
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
            string responseMessage = string.Empty;
            bool successStatus = false;

            switch (newMemberType)
            {
                case MemberType.Administrator:
                    responseMessage = "There may only be one administrator";
                    break;

                case MemberType.Moderator:
                    if (requestorMemberType != MemberType.Administrator)
                    {
                        responseMessage = "Only the administrators can promote to moderator.";
                    }
                    else
                    {
                        modifiedUserMembership.MemberType = MemberType.Moderator;
                        responseMessage = "User status changed to moderator.";
                        successStatus = true;
                    }
                    break;

                case MemberType.Editor:
                    modifiedUserMembership.MemberType = MemberType.Editor;
                    responseMessage = "User status changed to editor.";
                    successStatus = true;
                    break;

                case MemberType.Viewer:
                    modifiedUserMembership.MemberType = MemberType.Viewer;
                    responseMessage = "User status changed to viewer.";
                    successStatus = true;
                    break;
            }

            
            if(!successStatus)
            {
                responseMessage = string.IsNullOrEmpty(responseMessage) ? "There was an error while changing the member type" : responseMessage;
                return ServiceResponse.Error(responseMessage);
            }
            else
            {
                await _bookAnalysisUserRepository.Edit(modifiedUserMembership);
                await _hubServerService.AnalysisMemberModified(modifiedUserMembership.BookAnalysisId, modifiedUserMembership.UsersId, modifiedUserMembership.MemberType);
                return ServiceResponse.Success(responseMessage);
            }
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

            await _hubServerService.AnalysisMemberAdded(bookAnalysisId, invitedUserId);
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
                    await _hubServerService.AnalysisMemberRemoved(bookAnalysisId, removedUserId);
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
                        await _hubServerService.AnalysisMemberRemoved(bookAnalysisId, removedUserId);
                        return ServiceResponse.Success("User removed.");
                    }
                    else
                    {
                        return ServiceResponse.Error("Only the administrator can remove moderators.");
                    }

                default:
                    await _bookAnalysisUserRepository.Delete(modifiedUserMembership);
                    await _hubServerService.AnalysisMemberRemoved(bookAnalysisId, removedUserId);
                    return ServiceResponse.Success("User removed.");
            }
        }

        public async Task<ServiceResponse> AcceptInvite(int bookAnalysisId)
        {
            var userInvitation = await _bookAnalysisUserRepository
                .FindByConditionsFirstOrDefault(au => 
                   au.UsersId == _appUserService.GetCurrentUserId() 
                && au.BookAnalysisId == bookAnalysisId 
                && au.MemberType == MemberType.Invited);

            if (userInvitation is null)
            {
                return ServiceResponse.Error("User not invited.");
            }

            userInvitation.MemberType = MemberType.Viewer;
            await _bookAnalysisUserRepository.Edit(userInvitation);

            await _hubServerService.AnalysisMemberModified(bookAnalysisId, userInvitation.UsersId, userInvitation.MemberType);
            return ServiceResponse.Success("Invitation accepted.");
        }

        public async Task<ServiceResponse> DeclineInvite(int bookAnalysisId)
        {
            var userInvitation = await _bookAnalysisUserRepository
                .FindByConditionsFirstOrDefault(au =>
                    au.UsersId == _appUserService.GetCurrentUserId()
                 && au.BookAnalysisId == bookAnalysisId
                 && au.MemberType == MemberType.Invited);

            if (userInvitation is null)
            {
                return ServiceResponse.Error("User not invited.");
            }

            await _bookAnalysisUserRepository.Delete(userInvitation);

            await _hubServerService.AnalysisMemberRemoved(bookAnalysisId, userInvitation.UsersId);
            return ServiceResponse.Success("Invitation declined.");
        }
    }
}
