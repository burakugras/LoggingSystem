using Business.Abstracts;
using Business.Dtos.Requests.UserRequests;
using Core.CrossCutingConcerns.Logging.SeriLog.Logger;
using Core.CrossCutingConcerns.Logging;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(LoggingFilter))]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IUserDal _userDal;

        public AuthController(IAuthService authService,  IUserDal userDal)
        {
            _authService = authService;
            _userDal = userDal;
        }

        [Logging(typeof(MsSqlLogger))]
        [Logging(typeof(FileLogger))]
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

        [Logging(typeof(MsSqlLogger))]
        [Logging(typeof(FileLogger))]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserForLoginRequest userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }


            var result = _authService.CreateAccessToken(userToLogin.Data);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
