using MongoAuthenticatorAPI.Dtos;

namespace MongoAuthenticatorAPI.Services
{
  public interface ITaskService
  {
    Task<ServiceResponse<TaskItem>> AddTaskToProjectAsync(CreateTaskRequest request);
    Task<ServiceResponse<TaskItem>> UpdateTaskAsync(string projectId, string taskId, UpdateTaskRequest request);
    Task<ServiceResponse<bool>> DeleteTaskAsync(string projectId, string taskId);
    Task<ServiceResponse<TaskItem>> UpdateTaskDescriptionAndProgressAsync(string projectId, string taskId, UpdateTaskDescriptionAndProgressRequest request, string userEmail);
  }
}
