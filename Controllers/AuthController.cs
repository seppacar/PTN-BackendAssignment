using Microsoft.AspNetCore.Mvc;
using PTN_BackendAssignment.DTOs;
using PTN_BackendAssignment.Services;

namespace PTN_BackendAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthDTO authDTO)
        {
            try
            {
                var token = await _authService.Login(authDTO);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthDTO authDTO)
        {
            try
            {
                var token = await _authService.Register(authDTO);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
