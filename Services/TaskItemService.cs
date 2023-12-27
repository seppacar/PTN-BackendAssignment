using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PTN_BackendAssignment.Data;
using PTN_BackendAssignment.DTOs;
using PTN_BackendAssignment.Models;

namespace PTN_BackendAssignment.Services
{
    public class TaskItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaskItemService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new TaskItem.
        /// </summary>
        /// <param name="taskItemDTO">The DTO containing data for the new TaskItem.</param>
        /// <param name="ownerId">The owner's ID for the TaskItem.</param>
        /// <returns>The created TaskItem DTO.</returns>
        public async Task<TaskItemReadDTO> CreateTaskItem(TaskItemCreateDTO taskItemDTO, int ownerId)
        {
            // Map DTO to entity
            var newTaskItem = _mapper.Map<TaskItem>(taskItemDTO);

            // Set ownerId
            newTaskItem.OwnerId = ownerId;

            // Calculate DueDate
            newTaskItem.CalculateDueDate();

            // Add the new entity to the DbContext
            await _context.TasksItems.AddAsync(newTaskItem);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Include navigation property for Owner and retrieve the created TaskItem
            var createdTaskItem = await _context.TasksItems.Include(t => t.Owner).FirstOrDefaultAsync(t => t.Id == newTaskItem.Id);

            // Map the created entity back to a DTO and return it
            return _mapper.Map<TaskItemReadDTO>(createdTaskItem);
        }

        /// <summary>
        /// Get all TaskItems.
        /// </summary>
        /// <returns>A list of TaskItem DTOs.</returns>
        public async Task<List<TaskItemReadDTO>> GetAllTaskItems()
        {
            var taskItems = await _context.TasksItems.ToListAsync();

            return _mapper.Map<List<TaskItemReadDTO>>(taskItems);
        }

        /// <summary>
        /// Get a TaskItem by its ID.
        /// </summary>
        /// <param name="taskId">The ID of the TaskItem to retrieve.</param>
        /// <returns>The TaskItem DTO.</returns>
        public async Task<TaskItemReadDTO> GetTaskItemById(int taskId)
        {
            var taskItem = await _context.TasksItems.SingleOrDefaultAsync(task => task.Id == taskId);

            // Check if the TaskItem exists
            if (taskItem == null)
            {
                throw new Exception("Task not found");
            }

            return _mapper.Map<TaskItemReadDTO>(taskItem);
        }

        /// <summary>
        /// Get all TaskItems for a specific user by UserID.
        /// </summary>
        /// <param name="userId">The ID of the user whose TaskItems to retrieve.</param>
        /// <returns>A list of TaskItem DTOs for the specified user.</returns>
        public async Task<List<TaskItemReadDTO>> GetUserTaskItemsByUserId(int userId)
        {
            var userTaskItems = await _context.TasksItems.Where(taskItem => taskItem.OwnerId == userId).ToListAsync();

            return _mapper.Map<List<TaskItemReadDTO>>(userTaskItems);
        }

        /// <summary>
        /// Update an existing TaskItem.
        /// </summary>
        /// <param name="taskId">The ID of the TaskItem to update.</param>
        /// <param name="taskItemDTO">The DTO containing updated data for the TaskItem.</param>
        /// <returns>Task representing the completion of the update operation.</returns>
        public async Task UpdateTaskItem(int taskId, TaskItemUpdateDTO taskItemDTO)
        {
            var existingTaskItem = await _context.TasksItems.FindAsync(taskId);

            // Check if the TaskItem exists
            if (existingTaskItem != null)
            {
                // Recalculate DueDate
                existingTaskItem.CalculateDueDate();

                // Update properties of existing TaskItem based on DTO
                _mapper.Map(taskItemDTO, existingTaskItem);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a TaskItem by its ID.
        /// </summary>
        /// <param name="taskId">The ID of the TaskItem to delete.</param>
        /// <returns>Task representing the completion of the delete operation.</returns>
        public async Task DeleteTaskItem(int taskId)
        {
            var taskItemToDelete = await _context.TasksItems.FindAsync(taskId);

            // Check if the TaskItem exists
            if (taskItemToDelete != null)
            {
                _context.TasksItems.Remove(taskItemToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
