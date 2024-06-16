using MongoAuthenticatorAPI.Dtos;
using MongoAuthenticatorAPI.Models;

namespace MongoAuthenticatorAPI.Services
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(CreateProjectRequest request);
        Task<Project> GetProjectByIdAsync(string projectId);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<TaskItem> AddTaskToProjectAsync(CreateTaskRequest request);
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        Task<Project> UpdateProjectAsync(string id, UpdateProjectRequest request);
        Task<bool> DeleteProjectAsync(string id);
        Task<TaskItem> UpdateTaskAsync(string projectId, string taskId, UpdateTaskRequest request);
        Task<bool> DeleteTaskAsync(string projectId, string taskId);
        Task<TaskItem> UpdateTaskDescriptionAndProgressAsync(string projectId, string taskId, UpdateTaskDescriptionAndProgressRequest request);
    }
}
