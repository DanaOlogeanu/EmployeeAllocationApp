using Domain.Dtos;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ITaskProjectService
{
    Task<TaskProject> CreateAsync(TaskProjectCreationDto dto);
    
    Task <TaskProjectBasicDto?> GetByIdAsync(int id);
    
    Task UpdateAsync(TaskProjectBasicDto dto);
    
    Task<IEnumerable<TaskProject>> GetTasksUser(string username);
    Task<IEnumerable<TaskProject>?> GetTasksByParameters(SearchTaskProjectParametersDto parameters);
    
    Task <TaskProjectBasicDto> GetBySeq(int projectId, int sequenceNo);
}