namespace TaskTraker.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }

        public ICollection<Board> Boards { get; set; }
    }
}
