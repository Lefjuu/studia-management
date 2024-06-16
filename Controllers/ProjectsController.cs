using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAuthenticatorAPI.Dtos;
using MongoAuthenticatorAPI.Services;

namespace MongoAuthenticatorAPI.Controllers
{
    [ApiController]
    [Route("api/v1/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            var project = await _projectService.CreateProjectAsync(request);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> GetProjectById(string id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }


        [HttpPost("{projectId}/tasks")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddTaskToProject(string projectId, [FromBody] CreateTaskRequest request)
        {
            try
            {
                request.ProjectId = projectId;
                var task = await _projectService.AddTaskToProjectAsync(request);
                return Ok(task);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpGet("users")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _projectService.GetUsersAsync();
            return Ok(users);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProject(string id, [FromBody] UpdateProjectRequest request)
        {
            var project = await _projectService.UpdateProjectAsync(id, request);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            var success = await _projectService.DeleteProjectAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPut("admin/{projectId}/tasks/{taskId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTask(string projectId, string taskId, [FromBody] UpdateTaskRequest request)
        {
            var task = await _projectService.UpdateTaskAsync(projectId, taskId, request);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpDelete("admin/{projectId}/tasks/{taskId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTask(string projectId, string taskId)
        {
            var success = await _projectService.DeleteTaskAsync(projectId, taskId);
            if (!success) return NotFound();
            return NoContent();
        }


        [HttpPut("{projectId}/tasks/{taskId}")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> UpdateTaskDescriptionAndProgressAsync(string projectId, string taskId, [FromBody] UpdateTaskDescriptionAndProgressRequest request)
        {
            var task = await _projectService.UpdateTaskDescriptionAndProgressAsync(projectId, taskId, request);
            if (task == null) return NotFound();
            return Ok(task);
        }
    }
}
