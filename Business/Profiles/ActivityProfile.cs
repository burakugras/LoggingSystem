using AutoMapper;
using Business.Dtos.Requests.ActivityRequests;
using Business.Dtos.Responses.ActivityResponses;
using Core.DataAccess.Paging;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<CreateActivityRequest, ActivityProfile>();
            CreateMap<Activity, CreatedActivityResponse>();

            CreateMap<Activity, GetListActivityResponse>().ForMember(destinationMember: response => response.Username,
                memberOptions: opt => opt.MapFrom(au => au.User.Username));
                
            CreateMap<Paginate<Activity>, Paginate<GetListActivityResponse>>();

            CreateMap<UpdateActivityRequest, Activity>().ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            CreateMap<Activity, UpdateActivityRequest>();
        }
    }
}
