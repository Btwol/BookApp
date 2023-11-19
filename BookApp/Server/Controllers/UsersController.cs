namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAppUserGetterService _apiUserGetterService;

        public UsersController(IAppUserGetterService apiUserGetterService)
        {
            _apiUserGetterService = apiUserGetterService;
        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<ServiceResponse> GetUserById(int userId)
        {
            return await _apiUserGetterService.GetUserById(userId);
        }

        [HttpGet("GetUserByEmail/{userEmail}")]
        public async Task<ServiceResponse> GetUserByEmail(string userEmail)
        {
            return await _apiUserGetterService.GetUserByEmail(userEmail);
        }
    }
}
