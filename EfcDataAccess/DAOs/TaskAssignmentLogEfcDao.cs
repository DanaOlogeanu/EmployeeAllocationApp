using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class TaskAssignmentLogEfcDao:ITaskAssignmentLogDao
{
    private readonly AppContext context;

    public TaskAssignmentLogEfcDao(AppContext context)
    {
        this.context = context;
    }


    public async Task<TaskAssignmentLog> CreateAsync(TaskAssignmentLog taskAssignmentLog)
    {
        EntityEntry<TaskAssignmentLog> added = await context.TaskAssignmentLogs.AddAsync(taskAssignmentLog);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<List<TaskAssignmentLog>> GetTaskAssignmentLogsForProject(int taskId)
    {
        IQueryable<TaskAssignmentLog> found = context.TaskAssignmentLogs
            .Include(t => t.TaskProject)
            .Where(u => u.TaskProjectId == taskId)
            .AsQueryable();
       
        List<TaskAssignmentLog> result = await found.ToListAsync();
        return result; 
       
    }
}