using MongoAuthenticatorAPI.Dtos;

namespace MongoAuthenticatorAPI.Services
{
  public interface ITaskService
  {
    Task<TaskItem> AddTaskToProjectAsync(CreateTaskRequest request);
    Task<TaskItem> UpdateTaskAsync(string projectId, string taskId, UpdateTaskRequest request);
    Task<bool> DeleteTaskAsync(string projectId, string taskId);
    Task<TaskItem> UpdateTaskDescriptionAndProgressAsync(string projectId, string taskId, UpdateTaskDescriptionAndProgressRequest request);
  }
}
