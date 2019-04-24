using System.Collections.Generic;
using System.Threading.Tasks;

using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;

namespace BugTrackingAPI.Contracts
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectByIdAsync(int id);
        Task<Project> CreateProjectAsync(Project project);
        Task<Project> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(int id);
        Task<IEnumerable<Project>> GetAllProjectsAsync(PagingParameter pagingParameter);
    }
}
