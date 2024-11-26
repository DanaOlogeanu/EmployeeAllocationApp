using Domain.Dtos;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ITaskApprovalLogic
{
    Task<TaskApproval> CreateAsync(TaskApprovalCreationDto dto);
    
    Task<IEnumerable<TaskApproval>> GetApprovalsManager(string username);
    
    Task UpdateAsync(TaskApprovalBasicDto dto);
    
    Task <TaskApprovalBasicDto?> GetByIdAsync(int id);
    
    Task<int> GetPendingApprovalAsync(string username);
    
    Task <List<TaskApprovalBasicDto>> GetByTaskIdAsync(int taskProjectId);
}