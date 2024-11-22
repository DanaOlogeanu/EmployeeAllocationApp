using Domain.Dtos;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IProjectLogic
{
    Task<Project> CreateAsync(ProjectCreationDto dto);
    Task <ProjectBasicDto> GetByIdAsync(int id);
    
    Task UpdateAsync(ProjectBasicDto dto);
    
    Task<IEnumerable<Project>> GetProjects(string username);
    
    Task<Project> DuplicateProject(ProjectBasicDto originalProject, string username);
    
    Task<List<ProjectBasicDto>>GetProjectsByTagAsync(string tag);
} 