using System.Collections.Generic;
using System.Threading.Tasks;

using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;

namespace BugTrackingAPI.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync(PagingParameter pagingParameter);
        Task<Project> GetProjectByIdAsync(int id);
        Task<Project> CreateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(int id);
        Task<Project> UpdateProjectAsync(Project project);
    }
}
