using AutoMapper;
using Business.Dtos.Requests.UserRequests;
using Business.Dtos.Responses.ActivityResponses;
using Business.Dtos.Responses.UserReponses;
using Core.DataAccess.Paging;
using Core.Entities.Concretes;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForLoginRequest, User>();
            CreateMap<User, CreatedUserResponse>();
            CreateMap<UserBase, User>();
            CreateMap<User, UserBase>();

            CreateMap<Activity, GetListActivityResponse>();

            CreateMap<User, GetListUserResponse>()
            .ForMember(dest=>dest.Activities, opt=>opt.MapFrom(src=>src.Activities));

            CreateMap<Paginate<User>, Paginate<GetListUserResponse>>();

            CreateMap<UpdateUserRequest, User>().ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            CreateMap<User, UpdatedUserResponse>();
        }
    }
}
