using BookApp.Server.Models.Identity;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace Tests.IntegrationTests
{
    public class ControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly HttpClient _HttpClient;
        protected readonly DataContext _context;
        protected readonly IConfiguration _configuration;

        public ControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture)
        {
            _HttpClient = fixture._httpClient;
            _context = fixture._context;
            _configuration = fixture._configuration;

            string testToken = SteupUser();
            _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", testToken);
        }

        protected string SteupUser()
        {
            var user  = _context.Set<AppUser>().Where(user => user.Id == 1).FirstOrDefault();
            if (user is null)
            {
                user = new AppUser { Id = 1, Email = "testEmail1@email.com", UserName = "testUser1", NormalizedUserName = "TESTUSER1" };
                _context.Add(user);
                _context.Add(new AppRole { Id = 1, Name = "User", NormalizedName = "USER" });
                _context.Add(new IdentityUserRole<int> { RoleId = 1, UserId = 1 });
                _context.SaveChanges();
                user = _context.Set<AppUser>().Where(user => user.Id == 1).FirstOrDefault();
            }
            
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(GenerateJwtSecurityToken(userClaims));
        }

        private JwtSecurityToken GenerateJwtSecurityToken(List<Claim> claims)
        {
            return new JwtSecurityToken(
                _configuration["Authentication:JwtIssuer"],
                _configuration["Authentication:JwtAudience"],
                claims,
                expires: DateTime.Now.AddDays(double.Parse(_configuration["Authentication:JwtExpireDays"])),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JwtKey"])),
                        SecurityAlgorithms.HmacSha256
                )
            );
        }

        protected async Task<T> DeserializeResponse<T>(HttpResponseMessage response) where T : class
        {
            var resp = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(resp);
        }

        protected async Task AddToDatabase(IEnumerable<IDbModel> models)
        {
            await _context.AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }

        protected async Task<IDbModel> AddToDatabase(IDbModel model)
        {
            var addedModel = await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return addedModel.Entity;
        }

        protected void AssertResponseSuccess(ServiceResponse responseContent)
        {
            Assert.True(responseContent.SuccessStatus, responseContent.Message);
        }

        protected void AssertResponseFailure(ServiceResponse responseContent)
        {
            Assert.False(responseContent.SuccessStatus, responseContent.Message);
        }

        protected virtual async Task<List<T>> FindInDatabaseByConditions<T>(Expression<Func<T, bool>> expresion) where T : class
        {
            return await _context.Set<T>().Where(expresion).ToListAsync();
        }

        protected virtual async Task<T> FindInDatabaseByConditionsFirstOrDefault<T>(Expression<Func<T, bool>> expresion) where T : class
        {
            return await _context.Set<T>().Where(expresion).FirstOrDefaultAsync();
        }
    }
}
