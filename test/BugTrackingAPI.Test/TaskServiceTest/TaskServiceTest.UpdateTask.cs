using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BugTrackingAPI.Contracts;
using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Enums;
using BugTrackingAPI.Services;

using Moq;

using Xunit;

namespace BugTrackingAPI.Test.TaskServiceTest
{
    public partial class TaskServiceTest
    {
        [Fact]
        public async Task ChangeTaskInStatusNew()
        {
            var task = new ProjectTask()
            {
                Id = DEFAULT_ID,
                Name = "taskNew",
                Description = "testNew",
                Priority = 2,
                StatusId = (int)Status.InWork,
                ProjectId = 2
            };

            var updatedTask = await UpdateTaskAsync(Status.New, task);

            Assert.NotNull(updatedTask);
            Assert.True(EqualTasks(task, updatedTask));
        }

        [Fact]
        public async Task ChangeTaskInStatusWork()
        {
            var task = new ProjectTask()
            {
                Id = DEFAULT_ID,
                Name = "taskNew",
                Description = "testNew",
                Priority = 2,
                StatusId = (int)Status.InWork,
                ProjectId = 2
            };

            var updatedTask = await UpdateTaskAsync(Status.InWork, task);

            Assert.NotNull(updatedTask);
            Assert.True(EqualTasks(task, updatedTask));
        }

        [Fact]
        public async Task ChangeTaskInStatusClose()
        {
            var task = new ProjectTask()
            {
                Id = DEFAULT_ID,
                Name = "taskNew",
                Description = "testNew",
                Priority = 3,
                StatusId = (int)Status.Close,
                ProjectId = 3
            };

            var updatedTask = await UpdateTaskAsync(Status.Close, task);

            Assert.Null(updatedTask);
        }

        [Fact]
        public async Task ChangeTaskWithNotEqualsId()
        {
            var task = new ProjectTask()
            {
                Id = 5,
                Name = "taskNew",
                Description = "testNew",
                Priority = 3,
                StatusId = (int)Status.InWork,
                ProjectId = 1
            };

            var updatedTask = await UpdateTaskAsync(Status.New, task);

            Assert.Null(updatedTask);
        }

        public async Task<ProjectTask> GetTaskById(int id, int statusId)
        {
            var tasks = new List<ProjectTask>() { new ProjectTask()
            {
                Id = DEFAULT_ID,
                Name = "task",
                Description = "test",
                Priority = 1,
                StatusId = statusId,
                ProjectId = 1
            } };
            return await Task.FromResult(tasks.FirstOrDefault(x => x.Id == id));
        }

        private async Task<ProjectTask> UpdateTask(ProjectTask task)
        {
            return await Task.FromResult(new ProjectTask()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Priority = task.Priority,
                StatusId = task.StatusId,
                ProjectId = task.ProjectId
            });
        }

        private async Task<ProjectTask> UpdateTaskAsync(Status status, ProjectTask task)
        {
            var moq = new Mock<IRepositoryWrapper>();

            moq.Setup(repo => repo.Task.GetTaskByIdAsync(It.IsAny<int>())).Returns((int id) => GetTaskById(id, (int)status));
            moq.Setup(repo => repo.Task.UpdateTaskAsync(It.IsAny<ProjectTask>())).Returns((ProjectTask t) => UpdateTask(t));

            var taskService = new TaskService(moq.Object);
            return await taskService.UpdateTaskAsync(task);
        }
    }
}
