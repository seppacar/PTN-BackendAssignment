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

        [HttpPost]
        public async Task<IActionResult> Login(AuthDTO authDTO)
        {
            try
            {
                var token = await _authService.login(authDTO);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
