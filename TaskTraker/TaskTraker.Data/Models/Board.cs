namespace TaskTraker.Data.Models
{
    public class Board
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TaskItem>? Tasks { get; set; }

        public ICollection<User> Collaborators { get; set; } = [];
    }
}
