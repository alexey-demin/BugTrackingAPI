using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BugTrackingAPI.Contracts;
using BugTrackingAPI.Entities;
using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;
using BugTrackingAPI.Enums;

using Microsoft.EntityFrameworkCore;

namespace BugTrackingAPI.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly RepositoryContext _context;

        public TaskRepository(RepositoryContext repositoryContext)
        {
            _context = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
        }

        public async Task<ProjectTask> CreateTaskAsync(ProjectTask task)
        {
            _context.Task.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            if (!await TasksExists(id))
            {
                return false;
            }
            var toRemove = _context.Task.Find(id);
            _context.Task.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync(PagingParameter pagingParameter, TaskFilter filter,
            SortState sortOrder = SortState.Default)
        {
            IQueryable<ProjectTask> tasks = _context.Task;

            if (filter.Priority != null)
            {
                tasks = tasks.Where(t => t.Priority == filter.Priority);
            }
            if (filter.StatusId != null)
            {
                tasks = tasks.Where(t => t.StatusId == filter.StatusId);
            }
            if (filter.StartTimeCreate != null && filter.EndTimeCreate != null)
            {
                tasks = tasks.Where(t => t.CreateDate >= filter.StartTimeCreate && t.CreateDate <= filter.EndTimeCreate);
            }
            if (filter.StartTimeUpdate != null && filter.EndTimeUpdate != null)
            {
                tasks = tasks.Where(t => t.UpdateDate >= filter.StartTimeUpdate && t.UpdateDate <= filter.EndTimeUpdate);
            }

            return await SortProjectTaks(tasks, sortOrder).Include(t => t.Status).Include(t => t.Project).ToListAsync();
        }

        public async Task<ProjectTask> GetTaskByIdAsync(int id)
        {
            return await _context.Task.Include(t => t.Status).Include(t => t.Project).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int id, SortState sortOrder = SortState.Default)
        {
            var tasks = _context.Task.Where(t => t.ProjectId == id);
            return await SortProjectTaks(tasks, sortOrder).Include(t => t.Status).Include(t => t.Project).ToListAsync();
        }

        public async Task<ProjectTask> UpdateTaskAsync(ProjectTask task)
        {
            if (!await TasksExists(task.Id))
            {
                return null;
            }
            _context.Task.Update(task);

            _context.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        private IQueryable<ProjectTask> SortProjectTaks(IQueryable<ProjectTask> tasks, SortState sortOrder)
        {
            switch (sortOrder)
            {
                case SortState.CreateDateAsc:
                    tasks = tasks.OrderBy(task => task.CreateDate);
                    break;
                case SortState.CreateDateDesc:
                    tasks = tasks.OrderByDescending(task => task.CreateDate);
                    break;
                case SortState.UpdateDateAsc:
                    tasks = tasks.OrderBy(task => task.UpdateDate);
                    break;
                case SortState.UpdateDateDesc:
                    tasks = tasks.OrderByDescending(task => task.UpdateDate);
                    break;
                case SortState.PriorityAsc:
                    tasks = tasks.OrderBy(task => task.Priority);
                    break;
                case SortState.PriorityDesс:
                    tasks = tasks.OrderByDescending(task => task.Priority);
                    break;
                default:
                    break;
            }
            return tasks;
        }

        private async Task<bool> TasksExists(int id) => await GetTaskByIdAsync(id) != null;
    }
}
