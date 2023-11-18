using BookApp.Shared.Models.Identity;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;//

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<ServiceResponse> Register([FromBody] RegisterRequest registerRequest)
        {
            return await _accountService.RegisterUser(registerRequest);
        }

        [HttpPost("Login")]
        public async Task<ServiceResponse> LoginUser([FromBody] LoginRequest model)
        {
            return await _accountService.LoginUser(model);
        }
    }
}
