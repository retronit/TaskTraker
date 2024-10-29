using Microsoft.AspNetCore.Identity;

namespace TaskTraker.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<TaskItem>? Tasks { get; set; }

        public ICollection<Board>? Boards { get; set; }
    }
}
