using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BugTrackingAPI.Contracts;
using BugTrackingAPI.Entities.Extensions;
using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;

namespace BugTrackingAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepositoryWrapper _repository;

        public ProjectService(IRepositoryWrapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync(PagingParameter pagingParameter)
        {
            return await _repository.Project.GetAllProjectsAsync(pagingParameter);
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _repository.Project.GetProjectByIdAsync(id);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            var currentTime = DateTime.Now;
            project.CreateDate = currentTime;
            project.UpdateDate = currentTime;
            return await _repository.Project.CreateProjectAsync(project);
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            return await _repository.Project.DeleteProjectAsync(id);
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            var dbProject = await _repository.Project.GetProjectByIdAsync(project.Id);

            if (dbProject == null)
            {
                return null;
            }

            dbProject.CopyFrom(project);
            dbProject.UpdateDate = DateTime.Now;
            return await _repository.Project.UpdateProjectAsync(dbProject);
        }
    }
}
