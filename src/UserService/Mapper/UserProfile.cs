using AutoMapper;
using EventBusMQ.Events;
using UserService.Entities;

namespace UserService.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDetail, UpdateUserTokenEvent>().ReverseMap();
        }
    }
}
