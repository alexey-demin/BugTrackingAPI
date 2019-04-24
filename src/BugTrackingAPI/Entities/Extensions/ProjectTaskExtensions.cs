using BugTrackingAPI.Entities.Models;

namespace BugTrackingAPI.Entities.Extensions
{
    public static class ProjectTaskExtensions
    {
        public static void CopyFrom(this ProjectTask dbTask, ProjectTask task)
        {
            dbTask.Name = task.Name;
            dbTask.Description = task.Description;
            dbTask.Priority = task.Priority;
            dbTask.StatusId = task.StatusId;
            dbTask.ProjectId = task.ProjectId;
        }
    }
}
