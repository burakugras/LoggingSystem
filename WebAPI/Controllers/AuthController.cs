using Business.Abstracts;
using Business.Dtos.Requests.UserRequests;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(LoggingFilter))]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private readonly ActivityLoggerService _activityLogger;
        private IUserDal _userDal;

        public AuthController(IAuthService authService, ActivityLoggerService activityLogger, IUserDal userDal)
        {
            _authService = authService;
            _activityLogger = activityLogger;
            _userDal = userDal;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserForRegisterRequest userForRegisterDto)
        {
            var registerResult = await _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserForLoginRequest userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            //var user = await _userDal.GetAsync(u => u.Id == userToLogin.Data.Id);

            var result = _authService.CreateAccessToken(userToLogin.Data);

            if (result.Success)
            {
                //await _activityLogger.LogActivity(user.Id, "Login", "Kullanıcı başarıyla giriş yaptı.");
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
