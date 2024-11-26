using Domain.Dtos;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IProjectLogic
{
    Task<Project> CreateAsync(ProjectCreationDto dto);
    Task <ProjectBasicDto> GetByIdAsync(int id);
    
    Task UpdateAsync(ProjectBasicDto dto);
    
    Task<IEnumerable<Project>> GetProjects(string username);
    Task<IEnumerable<Project>> SearchProjectsAsync(SearchProjectParameters parameters);
} 