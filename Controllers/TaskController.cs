using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PTN_BackendAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult ProtectedEndpoint()
        {
            return Ok(new { id = 1 });
        }
    }
}
