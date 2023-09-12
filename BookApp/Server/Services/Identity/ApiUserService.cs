using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookApp.Server.Services.Interfaces.Identity;

namespace BookApp.Server.Services.Identity
{
    public class ApiUserService : IApiUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            IEnumerable<Claim> claims = GetUserClaims();
            var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return int.Parse(userIdClaim.Value);
        }

        public bool CurrentUserIsAdmin()
        {
            IEnumerable<Claim> claims = GetUserClaims();
            var userRoles = claims.Where(c => c.Type == ClaimTypes.Role);

            return userRoles.Any(r => r.Value == "Admin");
        }

        private IEnumerable<Claim> GetUserClaims()
        {
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authHeader.Substring("Bearer ".Length);

            var handler = new JwtSecurityTokenHandler();
            var claims = handler.ReadJwtToken(token).Claims;
            return claims;
        }
    }
}
