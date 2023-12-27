using AutoMapper;
using PTN_BackendAssignment.DTOs;
using PTN_BackendAssignment.Models;

namespace PTN_BackendAssignment.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<TaskItemCreateDTO, TaskItem>();
            CreateMap<TaskItemUpdateDTO, TaskItem>();
            CreateMap<TaskItem, TaskItemReadDTO>();
        }
    }
}
