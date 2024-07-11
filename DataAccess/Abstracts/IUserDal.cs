using Core.DataAccess.Repositories;
using Core.Entities.Concretes;
using Entities.Concretes;

namespace DataAccess.Abstracts
{
    public interface IUserDal : IRepository<User, int>, IAsyncRepository<User, int>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
