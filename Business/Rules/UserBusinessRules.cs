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
    public class UserBusinessRules:BaseBusinessRules
    {
        IUserDal _userDal;

        public UserBusinessRules(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task UserShouldNotExistsWithSameEmail(String email)
        {
            User? user = await _userDal.GetAsync(i => i.Email == email);
            if (user != null)
                throw new BusinessException(BusinessMessages.UserIsAlreadyExist);
        }
    }
}
