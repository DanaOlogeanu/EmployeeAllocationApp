using Domain.Models;

namespace Application.DaoInterfaces;

public interface ITaskApprovalDao
{
    Task<TaskApproval> CreateAsync(TaskApproval task);
    
    Task<IEnumerable<TaskApproval>> GetApprovalsManager(string username);
    Task UpdateAsync(TaskApproval approval);
    Task <TaskApproval?> GetByIdAsync(int id);
    Task<int> GetPendingApprovalAsync(string username);
}