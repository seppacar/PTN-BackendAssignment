using Microsoft.EntityFrameworkCore;
using PTN_BackendAssignment.Models;
using PTN_BackendAssignment.Data;
using PTN_BackendAssignment.DTOs;

namespace PTN_BackendAssignment.Services
{ 
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> register(AuthDTO authDto)
        {
            // Check if user with same email already exists
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email ==  authDto.Email);
            if (user != null)
            {
                throw new Exception("User with same email exists, please use another email address.");
            }

            // Initalize empty user object
            var userToCreate = new User();

            // Hash the password using BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(authDto.Password);

            // Set user objects fields
            userToCreate.PasswordHash = passwordHash;
            userToCreate.Email = authDto.Email;

            //
            var createdUser = await _context.Users.AddAsync(userToCreate);


            return "asd";
        }

        public async Task<string> login(AuthDTO authDTO)
        {
            var userEntity = await _context.Users.SingleOrDefaultAsync(u => u.Email == authDTO.Email);

            // If user is not found or password doesn't match throw exception
            if (userEntity == null || !BCrypt.Net.BCrypt.Verify(authDTO.Password, userEntity.PasswordHash))
            {
                throw new Exception("Authentication failed.");
            }

            // Generate token and return here

            return "";
        }

    }
}
