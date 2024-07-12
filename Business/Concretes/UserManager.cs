using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.UserRequests;
using Business.Dtos.Responses.UserReponses;
using Business.Rules;
using Core.DataAccess.Paging;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IMapper _mapper;
        UserBusinessRules _userBusinessRules;

        public UserManager(IUserDal userDal, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _mapper = mapper;
            _userDal = userDal;
            _userBusinessRules = userBusinessRules;
        }

        public void Add(UserBase user)
        {
            User userBase = _mapper.Map<User>(user);
            userBase.CreatedDate = DateTime.Now;
            _userDal.Add(userBase);
        }

        public async Task<UserBase> AddAsync(UserBase user)
        {
            User userBase = _mapper.Map<User>(user);
            User createdUser = await _userDal.AddAsync(userBase);
            CreatedUserResponse createdUserResponse = _mapper.Map<CreatedUserResponse>(createdUser);
            return user;
        }

        public async Task<User> DeleteAsync(int id)
        {
            await _userBusinessRules.IsExistsUser(id);
            var data = await _userDal.GetAsync(i => i.Id == id);
            var result = await _userDal.DeleteAsync(data);
            return result;
        }

        public async Task<IPaginate<GetListUserResponse>> GetAllAsync(PageRequest pageRequest)
        {
            var data = await _userDal.GetListAsync(include: p => p
            .Include(p => p.Activities),
            index: pageRequest.PageIndex,
            size: pageRequest.PageSize);

            var result = _mapper.Map<Paginate<GetListUserResponse>>(data);
            return result;

        }
        
        public async Task<GetListUserResponse> GetById(int id)
        {
            var data = await _userDal.GetAsync(predicate: c => c.Id == id,
            include: p => p.
            Include(p => p.Activities));

            var result = _mapper.Map<GetListUserResponse>(data);
            return result;
        }

        public async Task<UserBase> GetByMail(string email)
        {
            await _userBusinessRules.IsExistsUserByMail(email);
            var data = await _userDal.GetAsync(u => u.Email == email);
            UserBase result = _mapper.Map<UserBase>(data);
            return result;
        }

        public List<OperationClaim> GetClaims(UserBase user)
        {
            User userBase = _mapper.Map<User>(user);
            return _userDal.GetClaims(userBase);
        }

        public async Task<UpdatedUserResponse> UpdateAsync(UpdateUserRequest updateUserRequest)
        {
            await _userBusinessRules.IsExistsUser(updateUserRequest.Id);

            var data = await _userDal.GetAsync(i => i.Id == updateUserRequest.Id);
            _mapper.Map(updateUserRequest, data);
            data.UpdatedDate = DateTime.Now;
            await _userDal.UpdateAsync(data);
            var result = _mapper.Map<UpdatedUserResponse>(data);
            return result;
        }
    }
}
