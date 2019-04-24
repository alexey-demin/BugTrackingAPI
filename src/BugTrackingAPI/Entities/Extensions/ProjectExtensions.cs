using BugTrackingAPI.Entities.Models;

namespace BugTrackingAPI.Entities.Extensions
{
    public static class ProjectExtensions
    {
        public static void CopyFrom(this Project dbProject, Project project)
        {
            dbProject.Name = project.Name;
            dbProject.Description = project.Description;
        }
    }
}
