using Domain.Models;

namespace Application.DaoInterfaces;

public interface ITaskAssignmentLogDao
{
    Task<TaskAssignmentLog> CreateAsync(TaskAssignmentLog taskAssignmentLog);
    Task<List<TaskAssignmentLog>> GetTaskAssignmentLogsForProject(int taskId);
    
}