using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugTrackingAPI.Entities;
using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;
using BugTrackingAPI.Enums;
using BugTrackingAPI.Services;

using Microsoft.AspNetCore.Mvc;

namespace BugTrackingAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks([FromQuery]PagingParameter pagingParameter, [FromQuery]TaskFilter filter, [FromQuery]SortState sortOrder = SortState.Default)
        {
            return new ObjectResult(await _taskService.GetAllTasksAsync(pagingParameter, filter, sortOrder));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetTaskById(int id)
        {
            var project = await _taskService.GetTaskByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Не найдена задача с id = {id}");
            }

            return Ok(project);
        }

        [HttpGet("byProject/{id}")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasksByProjectId(int id, [FromQuery]SortState sortOrder = SortState.Default)
        {
            return new ObjectResult(await _taskService.GetTasksByProjectIdAsync(id, sortOrder));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectTask(int id, [FromBody]ProjectTask projectTask)
        {
            if (projectTask.Id != id)
            {
                ModelState.AddModelError("", "Не совпадают ID задач");
                return BadRequest(ModelState);
            }

            if (await _taskService.GetTaskByIdAsync(id) == null)
            {
                return NotFound($"Не найдена задача с id = {id}");
            }

            projectTask = await _taskService.UpdateTaskAsync(projectTask);
            if (projectTask != null)
            {
                return Ok(projectTask);
            }

            return StatusCode(500, "Не удалось выполнить обновление");
        }

        [HttpPost]
        public async Task<ActionResult<ProjectTask>> PostProjectTask([FromBody]ProjectTask projectTask)
        {
            return StatusCode(201, await _taskService.CreateTaskAsync(projectTask));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectTask>> DeleteProjectTask(int id)
        {
            if (await _taskService.GetTasksByProjectIdAsync(id) == null)
            {
                return NotFound($"Не найдена задача с id = {id}");
            }

            if (await _taskService.DeleteTaskAsync(id))
            {
                return Ok();
            }

            return StatusCode(500, "Не удалось выполнить удаление");
        }
    }
}
