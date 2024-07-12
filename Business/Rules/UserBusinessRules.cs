using Business.Messages;
using Core.Business.Rules;
using Core.CrossCutingConcerns.Exceptions.Types;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules
{
    public class UserBusinessRules : BaseBusinessRules
    {
        IUserDal _userDal;

        public UserBusinessRules(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task UserShouldNotExistsWithSameEmail(string email)
        {
            User? user = await _userDal.GetAsync(i => i.Email == email);
            if (user != null)
                throw new BusinessException(BusinessMessages.UserIsAlreadyExist);
        }
        public async Task UserShouldNotExistsWithSameUsername(string username)
        {
            User? user = await _userDal.GetAsync(i => i.Username == username);
            if (user != null)
                throw new BusinessException(BusinessMessages.UserIsAlreadyExist);
        }

        public async Task IsExistsUser(int id)
        {
            User user = await _userDal.GetAsync(u => u.Id == id);
            if (user == null)
            {
                throw new BusinessException(BusinessMessages.UserIsNotExist);
            }
        }
        public async Task IsExistsUserByMail(string email)
        {
            User user = await _userDal.GetAsync(u => u.Email == email);
            if (user == null)
            {
                throw new BusinessException(BusinessMessages.UserIsNotExist);
            }
        }
    }
}
