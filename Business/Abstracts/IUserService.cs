using Business.Dtos.Requests.UserRequests;
using Business.Dtos.Responses.UserReponses;
using Core.DataAccess.Paging;
using Core.Entities.Concretes;
using Core.Utilities.Results;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IUserService
    {
        Task<UserBase> AddAsync(UserBase user);
        Task<UpdatedUserResponse> UpdateAsync(UpdateUserRequest updateUserRequest);
        Task<DeletedUserResponse> DeleteAsync(int id);
        Task<IPaginate<GetListUserResponse>> GetAllAsync(PageRequest pageRequest);
        Task<GetListUserResponse> GetById(int id);
        List<OperationClaim> GetClaims(UserBase user);
        Task<UserBase> GetByMail(string email);
        void Add(UserBase user);
    }
}
    