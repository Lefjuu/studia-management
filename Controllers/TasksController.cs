using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAuthenticatorAPI.Dtos;
using MongoAuthenticatorAPI.Services;

namespace MongoAuthenticatorAPI.Controllers
{
    [ApiController]
    [Route("api/v1/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        public TaskController(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        [HttpPut("admin/{projectId}/{taskId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTask(string projectId, string taskId, [FromBody] UpdateTaskRequest request)
        {
            var response = await _taskService.UpdateTaskAsync(projectId, taskId, request);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpDelete("admin/{projectId}/{taskId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTask(string projectId, string taskId)
        {
            var response = await _taskService.DeleteTaskAsync(projectId, taskId);
            if (!response.Success) return NotFound(response.Message);
            return NoContent();
        }

        [HttpPut("{projectId}/{taskId}")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> UpdateTaskDescriptionAndProgressAsync(string projectId, string taskId, [FromBody] UpdateTaskDescriptionAndProgressRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _taskService.UpdateTaskDescriptionAndProgressAsync(projectId, taskId, request, userEmail);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);
        }
    }
}
