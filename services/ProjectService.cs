using MongoDB.Driver;
using MongoAuthenticatorAPI.Models;
using MongoAuthenticatorAPI.Dtos;
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

    }
}