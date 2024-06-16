using MongoAuthenticatorAPI.Dtos;
using MongoAuthenticatorAPI.Models;

namespace MongoAuthenticatorAPI.Services
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(CreateProjectRequest request);
        Task<Project> GetProjectByIdAsync(string projectId);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        Task<Project> UpdateProjectAsync(string id, UpdateProjectRequest request);
        Task<bool> DeleteProjectAsync(string id);
    }
}
