using MongoDB.Driver;
using MongoAuthenticatorAPI.Models;
using MongoAuthenticatorAPI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Project> GetProjectByIdAsync(string projectId)
    {
      return await _projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
    }

    public async Task<TaskItem> AddTaskToProjectAsync(CreateTaskRequest request)
    {
      var project = await GetProjectByIdAsync(request.ProjectId);
      if (project == null) throw new Exception("Project not found");

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

      return newTask;
    }

    public async Task<TaskItem> UpdateTaskAsync(string projectId, string taskId, UpdateTaskRequest request)
    {
      var project = await _projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
      if (project == null) return null;

      var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
      if (task == null) return null;

      task.Title = request.Title;
      task.Description = request.Description;
      task.Priority = request.Priority;
      task.UserId = request.UserId;
      task.Progress = request.Progress;


      await _projects.ReplaceOneAsync(p => p.Id == projectId, project);
      return task;
    }

    public async Task<bool> DeleteTaskAsync(string projectId, string taskId)
    {
      var project = await _projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
      if (project == null) return false;

      var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
      if (task == null) return false;

      project.Tasks.Remove(task);

      await _projects.ReplaceOneAsync(p => p.Id == projectId, project);
      return true;
    }
    public async Task<TaskItem> UpdateTaskDescriptionAndProgressAsync(string projectId, string taskId, UpdateTaskDescriptionAndProgressRequest request)
    {
      var project = await GetProjectByIdAsync(projectId);
      if (project == null)
      {
        return null;
      }

      var task = project.Tasks.Find(t => t.Id == taskId);
      if (task == null)
      {
        return null;
      }

      task.Description = request.Description;
      task.Progress = request.Progress;

      var update = Builders<Project>.Update.Set(p => p.Tasks, project.Tasks);
      await _projects.UpdateOneAsync(p => p.Id == projectId, update);

      return task;
    }

  }
}