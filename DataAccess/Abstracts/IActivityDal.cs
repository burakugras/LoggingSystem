using Core.DataAccess.Repositories;
using Entities.Concretes;

namespace DataAccess.Abstracts
{
    public interface IActivityDal: IRepository<Activity, int>, IAsyncRepository<Activity, int>
    {

    }
}
