namespace PTN_BackendAssignment.DTOs
{
    public class TaskItemCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskType TaskType { get; set; }
        public Priority Priority { get; set; }

    }
    public class TaskItemReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskType TaskType { get; set; }

        public Priority Priority { get; set; }

        public UserDTO? Owner { get; set; }

    }

    public class TaskItemUpdateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskType TaskType { get; set; }
        public Priority Priority { get; set; }
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum TaskType
    {
        Daily,
        Weekly,
        Monthly,
    }
}
