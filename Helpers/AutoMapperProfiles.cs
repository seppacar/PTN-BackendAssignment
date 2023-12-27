using AutoMapper;
using PTN_BackendAssignment.DTOs;
using PTN_BackendAssignment.Models;

namespace PTN_BackendAssignment.Helpers
{
    /// <summary>
    /// AutoMapper profiles to define mappings between DTOs (Data Transfer Objects) and entities.
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfiles"/> class.
        /// Configures mappings between UserDTO and User entity.
        /// </summary>
        public AutoMapperProfiles()
        {
            // Map from UserDTO to User entity
            CreateMap<UserDTO, User>()
                .ReverseMap(); // Also, allow reverse mapping from User entity to UserDTO

            // Map from TaskItemCreateDTO to TaskItem entity
            CreateMap<TaskItemCreateDTO, TaskItem>();

            // Map from TaskItemUpdateDTO to TaskItem entity
            CreateMap<TaskItemUpdateDTO, TaskItem>();

            // Map from TaskItem entity to TaskItemReadDTO
            CreateMap<TaskItem, TaskItemReadDTO>();
        }
    }
}
