using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using PTN_BackendAssignment.Data;
using PTN_BackendAssignment.DTOs;
using PTN_BackendAssignment.Helpers;
using PTN_BackendAssignment.Models;
using System.Security.Claims;

namespace PTN_BackendAssignment.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="authDto">The authentication DTO containing user data.</param>
        /// <returns>The generated JWT token upon successful registration.</returns>
        public async Task<string> Register(AuthDTO authDto)
        {
            // Check if user with the same email already exists
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == authDto.Email);
            if (user != null)
            {
                throw new Exception("User with the same email exists. Please use another email address.");
            }

            // Initialize an empty user object
            var newUser = new User();

            // Hash the password using BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(authDto.Password);

            // Set user object fields
            newUser.PasswordHash = passwordHash;
            newUser.Email = authDto.Email;

            // Add the new user to the database
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // Generate JWT token for the registered user
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, newUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = JwtHelper.GenerateToken(claims);

            return token;
        }

        /// <summary>
        /// Performs user login and generates a JWT token upon successful authentication.
        /// </summary>
        /// <param name="authDTO">The authentication DTO containing user credentials.</param>
        /// <returns>The generated JWT token upon successful authentication.</returns>
        public async Task<string> Login(AuthDTO authDTO)
        {
            // Find the user by email
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == authDTO.Email);

            // If user is not found or password doesn't match, throw an exception
            if (user == null || !BCrypt.Net.BCrypt.Verify(authDTO.Password, user.PasswordHash))
            {
                throw new Exception("Authentication failed. Invalid email or password.");
            }

            // Generate JWT token for the authenticated user
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = JwtHelper.GenerateToken(claims);

            return token;
        }
    }
}
