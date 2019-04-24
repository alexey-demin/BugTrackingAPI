using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BugTrackingAPI.Contracts;
using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Services;

using Moq;

using Xunit;

namespace BugTrackingAPI.Test.TaskServiceTest
{
    public partial class TaskServiceTest
    {
        [Fact]
        public async Task CreateTaskSuccess()
        {
            var task = new ProjectTask()
            {
                Id = 1,
                Name = "taskNew",
                Description = "testNew",
                Priority = 1,
                ProjectId = 1
            };

            var createdTask = await CreateTaskAsync(task);

            Assert.NotNull(createdTask);
        }

        [Fact]
        public async Task CreateTaskWithUncorrectProjectId()
        {
            var task = new ProjectTask()
            {
                Id = 1,
                Name = "taskNew",
                Description = "testNew",
                Priority = 1,
                ProjectId = 3
            };

            await Assert.ThrowsAsync<ArgumentException>(() => CreateTaskAsync(task));
        }

        private async Task<ProjectTask> CreateTaskAsync(ProjectTask task)
        {
            var moq = new Mock<IRepositoryWrapper>();

            moq.Setup(repo => repo.Task.CreateTaskAsync(It.IsAny<ProjectTask>())).Returns((ProjectTask t) => CreateTask(t));
            moq.Setup(repo => repo.Project.GetProjectByIdAsync(It.IsAny<int>())).Returns((int id) => GetProjectByID(id));

            var taskService = new TaskService(moq.Object);
            return await taskService.CreateTaskAsync(task);
        }

        private async Task<Project> GetProjectByID(int id)
        {
            var tasks = new List<Project>() { new Project()
            {
                Id = DEFAULT_ID,
                Name = "project",
                Description = "project"
            } };
            return await Task.FromResult(tasks.FirstOrDefault(x => x.Id == id));
        }

        private async Task<ProjectTask> CreateTask(ProjectTask task)
        {
            return await Task.FromResult(task);
        }
    }
}
