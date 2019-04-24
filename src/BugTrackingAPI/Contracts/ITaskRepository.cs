using System.Collections.Generic;
using System.Threading.Tasks;

using BugTrackingAPI.Entities;
using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;
using BugTrackingAPI.Enums;

namespace BugTrackingAPI.Contracts
{
    public interface ITaskRepository
    {
        Task<ProjectTask> GetTaskByIdAsync(int id);
        Task<ProjectTask> CreateTaskAsync(ProjectTask task);
        Task<ProjectTask> UpdateTaskAsync(ProjectTask task);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int id, SortState sortOrder = SortState.Default);
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync(PagingParameter pagingParameter, TaskFilter filter, SortState sortOrder = SortState.Default);
    }
}
