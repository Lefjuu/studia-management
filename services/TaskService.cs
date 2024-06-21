using MongoDB.Driver;
using MongoAuthenticatorAPI.Models;
using MongoAuthenticatorAPI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MongoAuthenticatorAPI.Services
{
  public class TaskService : ITaskService
  {
    private readonly IMongoCollection<Project> _projects;
    private readonly UserManager<ApplicationUser> _userManager;

    public TaskService(IMongoClient client, UserManager<ApplicationUser> userManager)
    {
      var database = client.GetDatabase("company-management-10");
      _projects = database.GetCollection<Project>("Projects");
      _userManager = userManager;
    }

    public async Task<ServiceResponse<Project>> GetProjectByIdAsync(string projectId)
    {
      var project = await _projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
      if (project == null)
      {
        return new ServiceResponse<Project>
        {
          Success = false,
          Message = "Project not found"
        };
      }
      return new ServiceResponse<Project> { Data = project };
    }

    public async Task<ServiceResponse<TaskItem>> AddTaskToProjectAsync(CreateTaskRequest request)
    {
      var projectResponse = await GetProjectByIdAsync(request.ProjectId);
      if (!projectResponse.Success) return new ServiceResponse<TaskItem> { Success = false, Message = projectResponse.Message };

      var project = projectResponse.Data;

      var newTask = new TaskItem
      {
        Id = MongoDBKeyGenerator.GenerateMongoDBKey(),
        Title = request.Title,
        Description = request.Description,
        Priority = request.Priority,
        UserId = request.UserId,
        ProjectId = request.ProjectId
      };

      project.Tasks.Add(newTask);
      await _projects.ReplaceOneAsync(p => p.Id == request.ProjectId, project);

      return new ServiceResponse<TaskItem> { Data = newTask };
    }

    public async Task<ServiceResponse<TaskItem>> UpdateTaskAsync(string projectId, string taskId, UpdateTaskRequest request)
    {
      var projectResponse = await GetProjectByIdAsync(projectId);
      if (!projectResponse.Success) return new ServiceResponse<TaskItem> { Success = false, Message = projectResponse.Message };

      var project = projectResponse.Data;
      var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
      if (task == null)
      {
        return new ServiceResponse<TaskItem> { Success = false, Message = "Task not found" };
      }

      task.Title = request.Title;
      task.Description = request.Description;
      task.Priority = request.Priority;
      task.UserId = request.UserId;
      task.Progress = request.Progress;

      await _projects.ReplaceOneAsync(p => p.Id == projectId, project);
      return new ServiceResponse<TaskItem> { Data = task };
    }

    public async Task<ServiceResponse<bool>> DeleteTaskAsync(string projectId, string taskId)
    {
      var projectResponse = await GetProjectByIdAsync(projectId);
      if (!projectResponse.Success) return new ServiceResponse<bool> { Success = false, Message = projectResponse.Message };

      var project = projectResponse.Data;
      var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
      if (task == null)
      {
        return new ServiceResponse<bool> { Success = false, Message = "Task not found" };
      }

      project.Tasks.Remove(task);
      await _projects.ReplaceOneAsync(p => p.Id == projectId, project);
      return new ServiceResponse<bool> { Data = true };
    }

    public async Task<ServiceResponse<TaskItem>> UpdateTaskDescriptionAndProgressAsync(string projectId, string taskId, UpdateTaskDescriptionAndProgressRequest request, string userEmail)
    {
      var projectResponse = await GetProjectByIdAsync(projectId);
      var user = await _userManager.FindByEmailAsync(userEmail);
      if (!projectResponse.Success) return new ServiceResponse<TaskItem> { Success = false, Message = projectResponse.Message };

      var project = projectResponse.Data;
      var task = project.Tasks.Find(t => t.Id == taskId);
      if (task == null)
      {
        return new ServiceResponse<TaskItem> { Success = false, Message = "Task not found" };
      }

      if (task.UserId != user.Id.ToString() && !user.Roles.Contains(Guid.Parse("d4b3b3f0-7f3b-4b6d-8b1b-3e1f0f3f3f3f")))
      {
        return new ServiceResponse<TaskItem> { Success = false, Message = "You are not allowed to update this task." };
      }

      task.Description = request.Description;
      task.Progress = request.Progress;

      var update = Builders<Project>.Update.Set(p => p.Tasks, project.Tasks);
      await _projects.UpdateOneAsync(p => p.Id == projectId, update);

      return new ServiceResponse<TaskItem> { Data = task };
    }
  }
}
