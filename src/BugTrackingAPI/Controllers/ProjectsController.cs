using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BugTrackingAPI.Entities.Models;
using BugTrackingAPI.Entities.Pagination;
using BugTrackingAPI.Services;

using Microsoft.AspNetCore.Mvc;

namespace BugTrackingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects([FromQuery]PagingParameter pagingParameter)
        {
            return new ObjectResult(await _projectService.GetAllProjectsAsync(pagingParameter));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Не найдена проект с id = {id}");
            }

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (project.Id != id)
            {
                ModelState.AddModelError("", "Не совпадают ID проектов");
                return BadRequest(ModelState);
            }

            if (await _projectService.GetProjectByIdAsync(id) == null)
            {
                return NotFound($"Не найден проект с id = {id}");
            }

            project = await _projectService.UpdateProjectAsync(project);
            if (project != null)
            {
                return Ok(project);
            }

            return StatusCode(500, "Не удалось выполнить обновление");
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            return StatusCode(201, await _projectService.CreateProjectAsync(project));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            if (await _projectService.GetProjectByIdAsync(id) == null)
            {
                return NotFound($"Не найден проект с id = {id}");
            }

            if (await _projectService.DeleteProjectAsync(id))
            {
                return Ok();
            }

            return StatusCode(500, "Не удалось выполнить удаление");
        }
    }
}
