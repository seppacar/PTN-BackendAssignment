using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PTN_BackendAssignment.Models
{
    // Enumeration representing the priority of a task
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    // Enumeration representing the type of a task (Daily, Weekly, Monthly)
    public enum TaskType
    {
        Daily,
        Weekly,
        Monthly,
    }

    // Enumeration representing the status of a task
    public enum Status
    {
        Todo,
        InProgress,
        Completed
    }

    // Represents a task item in the application
    public class TaskItem
    {
        // Primary key for the task item
        [Key]
        public int Id { get; set; }

        // Required: Title of the task
        [Required]
        public string Title { get; set; } = string.Empty;

        // Description of the task
        public string Description { get; set; } = string.Empty;

        // Required: Type of the task (Daily, Weekly, Monthly)
        [Required]
        public TaskType TaskType { get; set; }

        // Required: Priority of the task (Low, Medium, High)
        [Required]
        public Priority Priority { get; set; }

        // Due date for the task (private setter)
        public DateTime DueDate { get; private set; }

        // Date and time when the task was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Owner ID for the task
        public int OwnerId { get; set; }

        // Navigation property to the owner of the task
        public User? Owner { get; set; }

        // Status of the task (Todo, InProgress, Completed)
        public Status Status { get; set; } = Status.Todo;

        // Method to calculate the due date based on the task type
        public void CalculateDueDate()
        {
            var now = DateTime.Now;

            // Calculate due date based on the task type
            switch (this.TaskType)
            {
                case TaskType.Daily:
                    // Due date is the end of the current day
                    this.DueDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                    break;
                case TaskType.Weekly:
                    // Due date is the end of the current week
                    DayOfWeek currentDayOfWeek = now.DayOfWeek;
                    int daysUntilSunday = 7 - (int)currentDayOfWeek;
                    this.DueDate = now.AddDays(daysUntilSunday).Date.AddDays(1).AddSeconds(-1);
                    break;
                case TaskType.Monthly:
                    // Due date is the end of the current month
                    this.DueDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59);
                    break;
                default:
                    // Handle other cases or set a default value for DueDate
                    this.DueDate = DateTime.MaxValue;
                    break;
            }
        }
    }
}
