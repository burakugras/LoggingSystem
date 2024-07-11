using Business.Abstracts;
using Business.Dtos.Requests.UserRequests;
using Business.Messages;
using Business.Rules;
using Core.CrossCutingConcerns.Exceptions.Types;
using Core.Entities.Concretes;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private UserBusinessRules _userBusinessRules;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userBusinessRules = userBusinessRules;
        }

        public IDataResult<AccessToken> CreateAccessToken(UserBase user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, BusinessMessages.CreatedMessage);
        }

        public async Task<IDataResult<UserBase>> Login(UserForLoginRequest userForLoginDto)
        {
            var userToCheck = await _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                throw new BusinessException(BusinessMessages.EmailOrPasswordIsWrong);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                throw new BusinessException(BusinessMessages.EmailOrPasswordIsWrong);
            }

            return new SuccessDataResult<UserBase>(userToCheck, BusinessMessages.OkayMessage);
        }

        public async Task<IDataResult<UserBase>> Register(UserForRegisterRequest userForRegisterDto, string password)
        {
            await _userBusinessRules.UserShouldNotExistsWithSameEmail(userForRegisterDto.Email);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new UserBase
            {
                Email = userForRegisterDto.Email,
                Username = userForRegisterDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            await _userService.AddAsync(user);
            return new SuccessDataResult<UserBase>(user, BusinessMessages.OkayMessage);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(BusinessMessages.UserIsAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
