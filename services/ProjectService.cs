using MongoDB.Driver;
using MongoAuthenticatorAPI.Models;
using MongoAuthenticatorAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MongoAuthenticatorAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMongoCollection<Project> _projects;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectService(IMongoClient client, UserManager<ApplicationUser> userManager)
        {
            var database = client.GetDatabase("company-management-10");
            _projects = database.GetCollection<Project>("Projects");
            _userManager = userManager;
        }

        public async Task<Project> CreateProjectAsync(CreateProjectRequest request)
        {
            var newProject = new Project
            {
                Name = request.Name,
                Description = request.Description
            };
            await _projects.InsertOneAsync(newProject);
            return newProject;
        }

        public async Task<Project> GetProjectByIdAsync(string projectId)
        {
            return await _projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projects.Find(p => true).ToListAsync();
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

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<Project> UpdateProjectAsync(string id, UpdateProjectRequest request)
        {
            var project = await _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (project == null) return null;

            project.Name = request.Name;
            project.Description = request.Description;

            await _projects.ReplaceOneAsync(p => p.Id == id, project);
            return project;
        }

        public async Task<bool> DeleteProjectAsync(string id)
        {
            var result = await _projects.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
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