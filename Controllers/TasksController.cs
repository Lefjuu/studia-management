using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAuthenticatorAPI.Dtos;
using MongoAuthenticatorAPI.Services;

namespace MongoAuthenticatorAPI.Controllers
{
    [ApiController]
    [Route("api/v1/projects")]
    public class TaskController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        public TaskController(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        [HttpPost("{projectId}/tasks")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddTaskToProject(string projectId, [FromBody] CreateTaskRequest request)
        {
            try
            {
                request.ProjectId = projectId;
                var task = await _taskService.AddTaskToProjectAsync(request);
                return Ok(task);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("admin/{projectId}/tasks/{taskId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTask(string projectId, string taskId, [FromBody] UpdateTaskRequest request)
        {
            var task = await _taskService.UpdateTaskAsync(projectId, taskId, request);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpDelete("admin/{projectId}/tasks/{taskId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTask(string projectId, string taskId)
        {
            var success = await _taskService.DeleteTaskAsync(projectId, taskId);
            if (!success) return NotFound();
            return NoContent();
        }


        [HttpPut("{projectId}/tasks/{taskId}")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> UpdateTaskDescriptionAndProgressAsync(string projectId, string taskId, [FromBody] UpdateTaskDescriptionAndProgressRequest request)
        {
            var task = await _taskService.UpdateTaskDescriptionAndProgressAsync(projectId, taskId, request);
            if (task == null) return NotFound();
            return Ok(task);
        }
    }
}
