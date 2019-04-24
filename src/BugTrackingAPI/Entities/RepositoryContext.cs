using BugTrackingAPI.Entities.Models;

using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Entities
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectTask> Task { get; set; }
        public DbSet<TaskStatus> TaskStatus { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }
    }
}
