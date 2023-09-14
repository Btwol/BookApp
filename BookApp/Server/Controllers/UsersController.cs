using BookApp.Shared.Models.Services;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IApiUserGetterService _apiUserGetterService;

        public UsersController(IApiUserGetterService apiUserGetterService)
        {
            _apiUserGetterService = apiUserGetterService;
        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<ServiceResponse> GetUserById(int userId)
        {
            return await _apiUserGetterService.GetUserById(userId);
        }
    }
}
