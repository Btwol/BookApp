using BookApp.Server.Models;
using BookApp.Server.Models.Identity;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tests.IntegrationTests
{
    public class ControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly HttpClient _HttpClient;
        protected readonly DataContext _context;
        protected readonly IConfiguration _configuration;
        protected AppUser TestUser { get; private set; }

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
            var role = _context.Set<AppRole>().Where(user => user.Id == 1).FirstOrDefault();
            if (role is null)
            {
                _context.Add(new AppRole { Name = "User", NormalizedName = "USER" });
                _context.SaveChanges();

                role = _context.Set<AppRole>().Where(role => role.Name == "User").FirstOrDefault();
            }

            var user = _context.Set<AppUser>().Where(user => user.Id == 1).FirstOrDefault();
            if (user is null)
            {
                user = new AppUser { Email = "testEmail1@email.com", UserName = "testUser1", NormalizedUserName = "TESTUSER1" };
                _context.Add(user);
                _context.SaveChanges();

                user = _context.Set<AppUser>().Where(user => user.UserName == "testUser1").FirstOrDefault();
                if (_context.Set<AppRole>().Where(user => user.Id == 1).FirstOrDefault() is null)
                {
                    _context.Add(new IdentityUserRole<int> { RoleId = role.Id, UserId = user.Id });
                    _context.SaveChanges();
                }

                user = _context.Set<AppUser>().Where(user => user.Id == 1).FirstOrDefault();
            }

            TestUser = user;

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

        protected async Task<BookAnalysis> AddTestAnalysisToDatabase()
        {
            var testAnalysis = new BookAnalysis { BookHash = "TestHash", AnalysisTitle = "TestAnalysis", Authors = "TestAuthor", BookTitle = "TestBook" };
            var createdAnalysis = await AddToDatabase(testAnalysis);
            AddToDatabase(new BookAnalysisUser { BookAnalysisId = testAnalysis.Id, UsersId = TestUser.Id, MemberType = MemberType.Administrator });

            return (BookAnalysis)createdAnalysis;
        }

        protected async Task<Tag> AddTestTagToDatabase()
        {
            var testAnalysis = await AddTestAnalysisToDatabase();
            var testTag = new Tag { Name = "TestTag", BookAnalysisId = testAnalysis.Id };
            var createdTag = await AddToDatabase(testTag);

            return (Tag)createdTag;
        }

        protected async Task<Highlight> AddTestHighlightToDatabase()
        {
            var testAnalysis = await AddTestAnalysisToDatabase();
            var testHighlight = new Highlight
            {
                BookAnalysisId = testAnalysis.Id,
                FirstNodeCharIndex = 1,
                LastNodeCharIndex = 2,
                LastNodeIndex = 1,
                FirstNodeIndex = 2,
                PageNumber = 1,
            };
            var createdHighlight = await AddToDatabase(testHighlight);

            return (Highlight)createdHighlight;
        }

        protected async Task<ServiceResponse<BookAnalysisDetailedModel>> GetBookAnalysisRequest(int bookAnalysisId)
        {
            var getUrl = $"/BookAnalysis/GetAnalysisById/{bookAnalysisId}";
            var getAnalysisResponse = await DeserializeResponse<ServiceResponse<BookAnalysisDetailedModel>>(
                await _HttpClient.GetAsync(getUrl)
            );
            return getAnalysisResponse;
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
