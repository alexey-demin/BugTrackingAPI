using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugTrackingAPI.Entities;
using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;
using BugTrackingAPI.Enums;

namespace BugTrackingAPI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync(PagingParameter pagingParameter, TaskFilter filter, SortState sortOrder = SortState.Default);
        Task<ProjectTask> GetTaskByIdAsync(int id);
        Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int id, SortState sortOrder = SortState.Default);
        Task<ProjectTask> CreateTaskAsync(ProjectTask task);
        Task<bool> DeleteTaskAsync(int id);
        Task<ProjectTask> UpdateTaskAsync(ProjectTask task);
    }
}
