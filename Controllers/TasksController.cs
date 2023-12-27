using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTN_BackendAssignment.Authorization;
using PTN_BackendAssignment.DTOs;
using PTN_BackendAssignment.Services;
using System.Security.Claims;

namespace PTN_BackendAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskItemService _taskItemService;

        public TasksController(TaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskItemCreateDTO taskItemDTO)
        {
            try
            {
                var userIdentity = User.Identity as ClaimsIdentity;

                var userId = Convert.ToInt32(userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                var createdTask = await _taskItemService.CreateTaskItem(taskItemDTO, userId);

                return CreatedAtAction(nameof(GetUserTaskById), new { taskId = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var userTasks = await _taskItemService.GetAllTaskItems();
                return Ok(userTasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetUserTaskById(int taskId)
        {
            try
            {
                var userTask = await _taskItemService.GetTaskItemById(taskId);
                return Ok(userTask);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserTasksByUserId(int userId)
        {
            try
            {
                var userTasks = await _taskItemService.GetUserTaskItemsByUserId(userId);
                return Ok(userTasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{taskId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserTask(int taskId, [FromBody] TaskItemUpdateDTO taskItemDTO)
        {
            try
            {
                // Sanitize Owner and OwnerId
                await _taskItemService.UpdateTaskItem(taskId, taskItemDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{taskId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserTask(int taskId)
        {
            try
            {
                await _taskItemService.DeleteTaskItem(taskId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
