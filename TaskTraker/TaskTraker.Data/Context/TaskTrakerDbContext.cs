using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Models;

namespace TaskApp.Data.Context
{
    public class TaskTrakerDbContext(DbContextOptions<TaskTrakerDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<Board> Boards { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<User>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Board>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Status>()
                .HasKey(t => t.Id);
        }
    }
}
