namespace TaskTraker.Data.Models
{
    public class Board
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<TaskItem>? Tasks { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
