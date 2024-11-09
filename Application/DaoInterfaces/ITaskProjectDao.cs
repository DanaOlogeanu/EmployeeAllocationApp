using Domain.Models;

namespace Application.DaoInterfaces;

public interface ITaskProjectDao
{
    Task<TaskProject> CreateAsync(TaskProject task);
    
    Task <TaskProject?> GetByIdAsync(int id);
    
    Task UpdateAsync(TaskProject project);
    
    Task<IEnumerable<TaskProject>> GetTasksUser(string username);
}