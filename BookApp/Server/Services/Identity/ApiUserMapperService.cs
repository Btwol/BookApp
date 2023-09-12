﻿using BookApp.Server.Models.Identity;

namespace BookApp.Server.Services.Identity
{
    public class ApiUserMapperService : IApiUserMapperService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ApiUserMapperService(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiUserResponseDto> MapGetApiUserResponseDto(AppUser user)
        {
            var mappedUser = _mapper.Map<ApiUserResponseDto>(user);
            mappedUser.UserRoles = (await _userManager.GetRolesAsync(user)).ToList();
            return mappedUser;
        }

        public LoginResponse MapUserLoginResponse(AppUser user, string token)
        {
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.Token = token;
            loginResponse.User = _mapper.Map<ApiUserResponseDto>(user);
            return loginResponse;
        }

        public AppUser MapUserRegisterRequest(RegisterRequest user)
        {
            return _mapper.Map<AppUser>(user);
        }
    }
}
