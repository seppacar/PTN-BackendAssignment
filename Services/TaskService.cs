namespace PTN_BackendAssignment.Services
{
    public class TaskService
    {
        public UserService _userService;

        public TaskService(UserService userService)
        {
            _userService = userService;
        }
    }
}
