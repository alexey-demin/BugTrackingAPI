using BugTrackingAPI.Entities.Models;

namespace BugTrackingAPI.Test.TaskServiceTest
{
    public partial class TaskServiceTest
    {
        private const int DEFAULT_ID = 1;

        private bool EqualTasks(ProjectTask task1, ProjectTask task2)
        {
            if (task1 == null && task2 == null) { return true; }
            if (task1 == null || task2 == null) { return false; }
            if (task1.Id == task2.Id
                && task1.Name == task2.Name
                && task1.Description == task2.Description
                && task1.Priority == task2.Priority
                && task1.StatusId == task2.StatusId
                && task1.ProjectId == task2.ProjectId)
            {
                return true;
            }
            return false;
        }
    }
}
