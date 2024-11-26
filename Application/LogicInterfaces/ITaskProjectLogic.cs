using Domain.Dtos;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ITaskProjectLogic
{
    Task<TaskProject> CreateAsync(TaskProjectCreationDto dto);
    
    Task <TaskProjectBasicDto?> GetByIdAsync(int id);
    
    Task UpdateAsync(TaskProjectBasicDto dto);
    
    Task<IEnumerable<TaskProject>> GetTasksUser(string username);
    Task<IEnumerable<TaskProject>> SearchTasksAsync(SearchTaskProjectParametersDto parameters);
    
}