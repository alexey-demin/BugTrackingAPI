using System;

using BugTrackingAPI.Contracts;
using BugTrackingAPI.Entities;

namespace BugTrackingAPI.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IProjectRepository _project;
        private ITaskRepository _task;
        private ITaskStatusRepository _taskStatus;

        public IProjectRepository Project
        {
            get
            {
                if (_project == null)
                {
                    _project = new ProjectRepository(_repoContext);
                }

                return _project;
            }
        }

        public ITaskRepository Task
        {
            get
            {
                if (_task == null)
                {
                    _task = new TaskRepository(_repoContext);
                }

                return _task;
            }
        }

        public ITaskStatusRepository TaskStatus
        {
            get
            {
                if (_taskStatus == null)
                {
                    _taskStatus = new TaskStatusRepository(_repoContext);
                }

                return _taskStatus;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
        }
    }
}
