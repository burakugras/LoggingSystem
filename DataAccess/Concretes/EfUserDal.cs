using Core.DataAccess.Repositories;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class EfUserDal:EfRepositoryBase<User,int,LogDBContext>,IUserDal
    {
        LogDBContext _logDbContext;

        public EfUserDal(LogDBContext context) : base(context)
        {
            _logDbContext = context;
        }
        public List<OperationClaim> GetClaims(User user)
        {

            var result = from operationClaim in _logDbContext.OperationClaims
                         join userOperationClaim in _logDbContext.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();


        }
    }
}
