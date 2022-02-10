using AutoMapper;
using CourseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDtos.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<LoginDto, User>();
            CreateMap<User, LoginResponseDto>();
            CreateMap<UserAddDto, User>();
            CreateMap<User, UserAddDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto > ();
            CreateMap<User, UserDto>();
            CreateMap<User, UserAddRepDto>();
        }
    }
}
