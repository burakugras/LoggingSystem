using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.ActivityRequests;
using Business.Dtos.Responses.ActivityResponses;
using Core.DataAccess.Paging;
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
    public class ActivityManager : IActivityService
    {
        IActivityDal _activityDal;
        IMapper _mapper;
        public ActivityManager(IActivityDal activityDal, IMapper mapper)
        {
            _mapper = mapper;
            _activityDal = activityDal;
        }
        public async Task<CreatedActivityResponse> AddAsync(CreateActivityRequest createActivityRequest)
        {
            Activity activity = _mapper.Map<Activity>(createActivityRequest);
            Activity createdActivity = await _activityDal.AddAsync(activity);
            CreatedActivityResponse createdActivityResponse = _mapper.Map<CreatedActivityResponse>(createdActivity);
            return createdActivityResponse;
        }

        public async Task<Activity> DeleteAsync(int id)
        {
            var data = await _activityDal.GetAsync(i => i.Id == id);
            var result = await _activityDal.DeleteAsync(data);
            return result;
        }

        public async Task<IPaginate<GetListActivityResponse>> GetAllAsync(PageRequest pageRequest)
        {
            var data = await _activityDal.GetListAsync(
                include: p => p.Include(p => p.User),
                
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
               );
            var result = _mapper.Map<Paginate<GetListActivityResponse>>(data);
            return result;
        }
                

        public async Task<GetListActivityResponse> GetById(int id)
        {
            var data = await _activityDal.GetAsync(c => c.Id == id);
            var result = _mapper.Map<GetListActivityResponse>(data);
            return result;
        }

        public async Task<UpdatedActivityResponse> UpdateAsync(UpdateActivityRequest updateActivityRequest)
        {
            var data = await _activityDal.GetAsync(i => i.Id == updateActivityRequest.Id);
            _mapper.Map(updateActivityRequest, data);
            data.UpdatedDate = DateTime.Now;
            await _activityDal.UpdateAsync(data);
            var result = _mapper.Map<UpdatedActivityResponse>(data);
            return result;
        }
    }
}
