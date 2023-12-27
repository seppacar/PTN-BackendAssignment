using PTN_BackendAssignment.Models;

namespace PTN_BackendAssignment.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new task item.
    /// </summary>
    public class TaskItemCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskType TaskType { get; set; }
        public Priority Priority { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for reading task item details.
    /// </summary>
    public class TaskItemReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskType TaskType { get; set; }
        public Priority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public UserDTO? Owner { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for updating an existing task item.
    /// </summary>
    public class TaskItemUpdateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskType TaskType { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
