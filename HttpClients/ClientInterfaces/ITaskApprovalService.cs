using Domain.Dtos;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ITaskApprovalService
{
    Task<TaskApproval> CreateAsync(TaskApprovalCreationDto dto);
    
    Task<IEnumerable<TaskApproval>> GetApprovalsManager(string username);
    
    Task UpdateAsync(TaskApprovalBasicDto dto);
    
    Task <TaskApprovalBasicDto?> GetByIdAsync(int id);
}