using Business.Dtos.Requests.ActivityRequests;
using Business.Dtos.Responses.ActivityResponses;
using Core.DataAccess.Paging;
using Entities.Concretes;

namespace Business.Abstracts
{
    public interface IActivityService
    {
        Task<CreatedActivityResponse> AddAsync(CreateActivityRequest createActivityRequest);
        Task<UpdatedActivityResponse> UpdateAsync(UpdateActivityRequest updateActivityRequest);
        Task<Activity> DeleteAsync(int id);
        Task<IPaginate<GetListActivityResponse>> GetAllAsync(PageRequest pageRequest);
        Task<GetListActivityResponse> GetById(int id);
    }
}
