using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PTN_BackendAssignment.Helpers
{
    public static class JwtHelper
    {
        private static string? JwtIssuer;
        private static string? JwtAudience;
        private static string? JwtSecret;
        private static double JwtExiprationHrs;

        // Initialize method to set configuration values
        public static void Initialize(IConfiguration configuration)
        {
            JwtIssuer = configuration["Issuer"] ?? string.Empty;
            JwtAudience = configuration["Audience"] ?? string.Empty;
            JwtSecret = configuration["Secret"] ?? string.Empty;
            var expirationHrs = configuration?["ExpirationHours"] ?? string.Empty;
            JwtExiprationHrs = double.Parse(expirationHrs);
        }

        /// <summary>
        /// Generates a JWT token based on the provided claims.
        /// </summary>
        /// <param name="claims">List of claims to include in the token.</param>
        /// <returns>The generated JWT token.</returns>
        public static string GenerateToken(List<Claim> claims)
        {
            if (string.IsNullOrEmpty(JwtIssuer) || string.IsNullOrEmpty(JwtAudience) || string.IsNullOrEmpty(JwtSecret))
            {
                throw new InvalidOperationException("JwtHelper not initialized with configuration.");
            }

            // Create a signing key from the secret
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));

            // Create the JWT token
            var token = new JwtSecurityToken(
                JwtIssuer,
                JwtAudience,
                claims,
                expires: DateTime.Now.AddHours(JwtExiprationHrs), // Adjust token expiration as needed
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            // Write the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
