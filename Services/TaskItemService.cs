using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PTN_BackendAssignment.Data;
using PTN_BackendAssignment.DTOs;
using PTN_BackendAssignment.Models;

namespace PTN_BackendAssignment.Services
{
    public class TaskItemService
    {
        public ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaskItemService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskItemReadDTO> CreateTaskItem(TaskItemCreateDTO userTaskDTO, int ownerId)
        {
            // Map UserTaskDTO to UserTask entity
            var newUserTask = _mapper.Map<TaskItem>(userTaskDTO);
            // Set ownerId
            newUserTask.OwnerId = ownerId;

            // Add the new UserTask entity to the DbContext
            await _context.TasksItems.AddAsync(newUserTask);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // w navs
            var createdTaskItem = await _context.TasksItems.Include(o => o.Owner).FirstOrDefaultAsync(t => t.Id == newUserTask.Id);

            // Map the created UserTask entity back to a UserTaskDTO and return it
            return _mapper.Map<TaskItemReadDTO>(createdTaskItem);
        }

        public async Task<List<TaskItemReadDTO>> GetAllTaskItems()
        {
            var userTasks = await _context.TasksItems.ToListAsync();

            return _mapper.Map<List<TaskItemReadDTO>>(userTasks);
        }

        public async Task<TaskItemReadDTO> GetTaskItemById(int taskId)
        {
            var userTask = await _context.TasksItems.SingleOrDefaultAsync(task => task.Id == taskId);
            if (userTask == null)
            {
                throw new Exception("Task not found");
            }

            return _mapper.Map<TaskItemReadDTO>(userTask);
        }

        public async Task<List<TaskItemReadDTO>> GetUserTaskItemsByUserId(int userId)
        {
            var userTasks = await _context.TasksItems.Where(userTask => userTask.OwnerId == userId).ToListAsync();

            return _mapper.Map<List<TaskItemReadDTO>>(userTasks);
        }

        public async Task UpdateTaskItem(int taskId, TaskItemUpdateDTO userTaskDTO)
        {
            var existingUserTask = await _context.TasksItems.FindAsync(taskId);

            if (existingUserTask != null)
            {
                // Update properties of existingUserTask based on userTaskDTO
                _mapper.Map(userTaskDTO, existingUserTask);

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskItem(int userTaskId)
        {
            var userTaskToDelete = await _context.TasksItems.FindAsync(userTaskId);

            if (userTaskToDelete != null)
            {
                _context.TasksItems.Remove(userTaskToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
