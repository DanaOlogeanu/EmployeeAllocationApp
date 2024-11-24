using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class TaskProjectEfcDao:ITaskProjectDao
{
    private readonly AppContext context;

    public TaskProjectEfcDao(AppContext context)
    {
        this.context = context;
    }

    
    public async Task<TaskProject> CreateAsync(TaskProject task)
    {
        
        
        Project? project = await context.Projects
              .Include( p => p.Tasks)
              .FirstOrDefaultAsync(p => p.ProjectId == task.ProjectId);
        if (project == null)
            throw new Exception("Project not found");
        
        // Determine the next sequence number
        int? nextSequenceNumber = project.Tasks.Any() ? project.Tasks.Max(t => t.SequenceNo) + 1 : 1;
        task.SequenceNo = nextSequenceNumber;

       
        //Determine the order number
        // Find the task that should come directly before the new task by using the provided OrderNo
        var previousTask = project.Tasks
            .Where(t => t.OrderNo < task.OrderNo)
            .OrderByDescending(t => t.OrderNo)
            .FirstOrDefault();
        
        double? newOrder = 0;
        if (task.OrderNo != null)
        {
            newOrder = task.OrderNo;
        }
        if (newOrder == 0 && previousTask != null)
        {
            newOrder = previousTask.OrderNo;
        }
        task.OrderNo = newOrder;

        if (task.DependentOn != null && task.DependentOn != 0)
        {
            var dependentOnTask = project.Tasks
                .Where(t => (int)t.OrderNo == task.DependentOn)
                .FirstOrDefault();
            if (dependentOnTask != null)
            {
                task.DependentOn = dependentOnTask.SequenceNo;
            }
            else
            {
                task.DependentOn = null;
            }
        }

        if (task.OrderNo % 1 != 0)
        {
            // Find the other tasks if they exists and update their order number
            var nextTasks = project.Tasks
                .Where(t => t.OrderNo > task.OrderNo)
                .OrderByDescending(t => t.OrderNo)
                .ToList();

            if (nextTasks != null)
            {
                foreach (var taskProject in nextTasks)
                {
                    taskProject.OrderNo = taskProject.OrderNo + 1;
                    await UpdateAsync(taskProject);
                }
            }

            if (task.OrderNo != null)
            {
                task.OrderNo = Math.Ceiling((double)task.OrderNo);
            }
        }

        EntityEntry<TaskProject> added = await context.TasksProject.AddAsync(task);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<TaskProject?> GetByIdAsync(int id)
    {
        TaskProject? found = await context.TasksProject
            //  .AsNoTracking()
            .Include(us => us.TaskSkills) 
            .Include(us =>us.Project)
            .SingleOrDefaultAsync(us=> us.Id == id);
        return found;
    }

    public async Task UpdateAsync(TaskProject taskProject)
    {
        context.TasksProject.Update(taskProject);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskProject>> GetTasksUser(string username)
    {
        IQueryable<TaskProject> query = context.TasksProject.AsQueryable();
            //.Include (us => us.Owner).AsQueryable();
        if (!string.IsNullOrEmpty(username))
        {
            query = query.Where(u => u.OwnerUsername == username);
        }
   
        List<TaskProject> result = await query.ToListAsync();
        return result; 
    }

    public async Task<TaskProject> GetBySeq(int projectId, int sequenceNo)
    {
        TaskProject? found = await context.TasksProject
            .Where (tp=>tp.ProjectId==projectId)
            .SingleOrDefaultAsync(us=> us.SequenceNo == sequenceNo);
        return found;
    }
}


 
  
