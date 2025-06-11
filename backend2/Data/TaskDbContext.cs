using BescoTaskApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BescoTaskApi.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }
        
        public DbSet<Models.Task> Tasks { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Task>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title).IsRequired().HasMaxLength(255);
                entity.Property(t => t.Description).HasMaxLength(500);
                entity.Property(t => t.Status).IsRequired().HasConversion<string>();
                entity.Property(t => t.CreatedAt).IsRequired();
                
                // Index for better performance on common queries
                entity.HasIndex(t => t.Status);
                entity.HasIndex(t => t.CreatedAt);
            });
        }
    }
} 