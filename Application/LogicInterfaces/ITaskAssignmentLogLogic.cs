using Domain.Dtos;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ITaskAssignmentLogLogic
{
    Task<TaskAssignmentLog> CreateAsync(TaskAssignmentLogCreationDto creationDto);
    Task<List<TaskAssignmentLogBasicDto>> GetTaskAssignmentLogsForProject(int taskId);
}