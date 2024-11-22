using Domain.Dtos;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ITaskAssignmentLogService
{
    Task<TaskAssignmentLog> CreateAsync(TaskAssignmentLogCreationDto creationDto);
    Task<List<TaskAssignmentLogBasicDto>> GetTaskAssignmentLogsForProject(int projectId);
    
}