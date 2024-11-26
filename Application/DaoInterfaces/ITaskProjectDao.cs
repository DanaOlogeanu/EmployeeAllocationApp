using Domain.Dtos;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ITaskProjectDao
{
    Task<TaskProject> CreateAsync(TaskProject task);
    
    Task <TaskProject?> GetByIdAsync(int id);
    
    Task UpdateAsync(TaskProject project);
    
    Task<IEnumerable<TaskProject>> GetTasksUser(string username);
    
    Task <TaskProject> GetBySeq(int projectId, int sequenceNo);
    
    

 
    Task<IEnumerable<TaskProject>> SearchTasksAsync(SearchTaskProjectParametersDto parameters); 
}