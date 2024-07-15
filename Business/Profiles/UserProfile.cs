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
                        CreateMap<CreatedUserResponse, UserBase>();
            CreateMap<UserBase, User>();
            CreateMap<User, UserBase>();

            CreateMap<User, GetListUserResponse>()
                .ForMember(destinationMember: response => response.ActivityType, memberOptions:
                opt => opt.MapFrom(acs => acs.Activities))
                .ForMember(destinationMember: response => response.Date, memberOptions:
                opt => opt.MapFrom(acs => acs.Activities))
                .ForMember(destinationMember: response => response.Description, memberOptions:
                opt => opt.MapFrom(acs => acs.Activities))
                .ReverseMap();

            CreateMap<Paginate<User>, Paginate<GetListUserResponse>>();

            CreateMap<UpdateUserRequest, User>().ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            CreateMap<User, UpdatedUserResponse>();
        }
    }
}
