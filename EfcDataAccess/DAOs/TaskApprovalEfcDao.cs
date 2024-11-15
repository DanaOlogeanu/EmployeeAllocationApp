using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class TaskApprovalEfcDao:ITaskApprovalDao
{
    private readonly AppContext context;

    public TaskApprovalEfcDao(AppContext context)
    {
        this.context = context;
    }


    public async Task<TaskApproval> CreateAsync(TaskApproval taskApproval)
    {
        EntityEntry<TaskApproval> added = await context.TasksApprovals.AddAsync(taskApproval);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<TaskApproval>> GetApprovalsManager(string username)
    {
        IQueryable<TaskApproval> query = context.TasksApprovals
        .Include(us => us.Owner).Include(us=>us.TaskProject).AsQueryable();
        if (!string.IsNullOrEmpty(username))
        {
            query = query.Where(u => u.OwnerUsername == username);
        }
   
        List<TaskApproval> result = await query.ToListAsync();
        return result; 
    }

    public async Task UpdateAsync(TaskApproval approval)
    {
        context.TasksApprovals.Update(approval);
        await context.SaveChangesAsync();
    }

    public async Task<TaskApproval?> GetByIdAsync(int id)
    {
        TaskApproval? found = await context.TasksApprovals
            //  .AsNoTracking()
            .Include(us => us.Owner) 
            .Include(us=>us.TaskProject)
            .SingleOrDefaultAsync(us=> us.Id == id);
        return found;
    }
}