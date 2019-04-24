using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BugTrackingAPI.Contracts;
using BugTrackingAPI.Entities;
using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;

using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly RepositoryContext _context;

        public ProjectRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync(PagingParameter pagingParameter)
        {
            return await _context.Project.Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize).Take(pagingParameter.PageSize).ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Project.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            if (!await ProjectExists(project.Id))
            {
                return null;
            }
            _context.Project.Update(project);

            _context.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            if (!await ProjectExists(id))
            {
                return false;
            }
            var toRemove = _context.Project.Find(id);

            _context.Project.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ProjectExists(int id) => await GetProjectByIdAsync(id) != null;
    }
}
