using PTN_BackendAssignment.Data;

namespace PTN_BackendAssignment.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
