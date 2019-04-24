using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BugTrackingAPI.Contracts;
using BugTrackingAPI.Entities;
using BugTrackingAPI.Entities.Extensions;
using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;
using BugTrackingAPI.Enums;

namespace BugTrackingAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepositoryWrapper _repository;

        public TaskService(IRepositoryWrapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync(PagingParameter pagingParameter, TaskFilter filter, SortState sortOrder = SortState.Default)
        {
            return await _repository.Task.GetAllTasksAsync(pagingParameter, filter, sortOrder);
        }

        public async Task<ProjectTask> GetTaskByIdAsync(int id)
        {
            return await _repository.Task.GetTaskByIdAsync(id);
        }

        public async Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int id, SortState sortOrder = SortState.Default)
        {
            return await _repository.Task.GetTasksByProjectIdAsync(id, sortOrder);
        }

        public async Task<ProjectTask> CreateTaskAsync(ProjectTask task)
        {
            if (await _repository.Project.GetProjectByIdAsync(task.ProjectId) == null)
            {
                throw new ArgumentException($"Проект, на который ссылается задача, отсутсвует в базе данных");
            }
            var currentTime = DateTime.Now;
            task.CreateDate = currentTime;
            task.UpdateDate = currentTime;
            task.StatusId = (int)Status.New;
            return await _repository.Task.CreateTaskAsync(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _repository.Task.DeleteTaskAsync(id);
        }

        public async Task<ProjectTask> UpdateTaskAsync(ProjectTask task)
        {
            var dbTask = await _repository.Task.GetTaskByIdAsync(task.Id);

            if (dbTask == null)
            {
                return null;
            }

            switch (dbTask.StatusId)
            {
                case (int)Status.New:
                case (int)Status.InWork:
                    var statusId = task.StatusId;
                    if (!(Enum.IsDefined(typeof(Status), statusId)))
                    {
                        return null;
                    }
                    break;
                case (int)Status.Close:
                    return null;
            }

            dbTask.CopyFrom(task);
            dbTask.UpdateDate = DateTime.Now;

            return await _repository.Task.UpdateTaskAsync(dbTask);
        }
    }
}
