using Domain.Dtos;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IProjectDao
{
    Task<Project> CreateAsync(Project project);
    Task <Project?> GetByIdAsync(int id);
    
    Task UpdateAsync(Project project);
    
    Task<IEnumerable<Project>> GetProjects(string username);

    Task<Project> DuplicateProject(Project originalProject, string username);

    Task <List<Project>> GetProjectsByTagAsync(string tag);
}