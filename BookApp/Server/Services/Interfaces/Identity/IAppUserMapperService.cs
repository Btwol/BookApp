﻿using BookApp.Shared.Models.Identity;

namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IAppUserMapperService
    {
        public Task<AppUserModel> MapGetApiUserResponseDto(AppUser user);
        public LoginResponse MapUserLoginResponse(AppUser user, string token);
        public AppUser MapUserRegisterRequest(RegisterRequest user);
    }
}
