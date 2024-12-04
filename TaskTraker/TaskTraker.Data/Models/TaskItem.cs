namespace TaskTraker.Data.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int BoardId { get; set; }

        public int StatusId { get; set; }

        public string? AssigneeId { get; set; }

        public Board Board { get; set; }

        public Status Status { get; set; }

        public User? Assignee { get; set; }
    }
}
