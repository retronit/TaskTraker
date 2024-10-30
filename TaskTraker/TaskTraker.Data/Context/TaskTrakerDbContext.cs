using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTraker.Data.Models;

namespace TaskTraker.Data.Context
{
    public class TaskTrakerDbContext(DbContextOptions<TaskTrakerDbContext> options) : IdentityDbContext<User>(options)
    { 

        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<Board> Boards { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<StatusTransition> StatusTransitions { get; set; }

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

            modelBuilder.Entity<StatusTransition>()
                .HasKey(t => t.Id);
        }
    }
}
