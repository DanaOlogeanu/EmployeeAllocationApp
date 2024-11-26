using Application.DaoInterfaces;
using Domain.Dtos;
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
    
        // Find the next task if it exists
        var nextTask = project.Tasks
            .Where(t => t.OrderNo > task.OrderNo)
            .OrderBy(t => t.OrderNo)
            .FirstOrDefault();

        // Calculate new OrderNo as midpoint between `previousTask` and `nextTask`
        double newOrder;
    
        if (previousTask == null && nextTask == null)
        {
            // No tasks exist yet, so this is the first task
            newOrder = 1;
        }
        else if (previousTask != null && nextTask != null)
        {
            // Position between previous and next task
            newOrder = (double)((previousTask.OrderNo + nextTask.OrderNo) / 2.0);
        }
        else if (previousTask != null)
        {
            // Place after the last task
            newOrder = (double)(previousTask.OrderNo + 1);
        }
        else // nextTask != null
        {
            // Place before the first task
            newOrder = (double)(nextTask.OrderNo - 1);
        }
    
        // Set the new task's OrderNo
        task.OrderNo = newOrder;
        
        
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
    
    
    
    

        public async Task<IEnumerable<TaskProject>> SearchTasksAsync(SearchTaskProjectParametersDto parameters)
        {
            IQueryable<TaskProject> query = context.TasksProject.AsQueryable();

            if (parameters.Id.HasValue)
            {
                query = query.Where(t => t.Id == parameters.Id);
            }
            if (!string.IsNullOrEmpty(parameters.TaskName))
            {
                query = query.Where(t => t.Name.Contains(parameters.TaskName));
            }
            if (!string.IsNullOrEmpty(parameters.OwnerUsername))
            {
                query = query.Where(t => t.OwnerUsername == parameters.OwnerUsername);
            }
            if (parameters.StartDate.HasValue)
            {
                query = query.Where(t => t.StartDate >= parameters.StartDate);
            }
            if (parameters.Deadline.HasValue)
            {
                query = query.Where(t => t.Deadline <= parameters.Deadline);
            }
            if (parameters.TaskStatus.HasValue)
            {
                query = query.Where(t => t.TaskStatusEnum == parameters.TaskStatus);
            }
            
            return await query.ToListAsync();
        }
    }



    // public async Task<Project> CreateAsync(Project project)
    // {
    //     // int seqNum = figureOutTheHighestSeqNumplusOne
    //     // project.setSeqNum(seqNum);
    //     //set dependency on seq number but order display
    //     EntityEntry<Project> added = await context.Projects.AddAsync(project);
    //     await context.SaveChangesAsync();
    //     return added.Entity;
    // }
    //
    // // public async int figureOutTheHighestSeqNumplusOne(long ProjectOID)
    // // {
    // //     // Task.|ProjId = proJid
    // //     // - highest no seq
    // //     //     -+1
    // //     //     
    // // }
    
    
    
    //example to set the seq number for create task 
    // public async Task<Task> CreateTaskAsync(int projectId, string description)
    // {
    //     // Retrieve the project by ID, including existing tasks
    //     var project = await _context.Projects
    //         .Include(p => p.Tasks)
    //         .FirstOrDefaultAsync(p => p.ProjectId == projectId);
    //
    //     if (project == null)
    //         throw new Exception("Project not found");
    //
    //     // Determine the next sequence number
    //     int nextSequenceNumber = project.Tasks.Any() ? project.Tasks.Max(t => t.SequenceNumber) + 1 : 1;
    //
    //     // Create a new task with the incremented sequence number
    //     var task = new Task
    //     {
    //         ProjectId = projectId,
    //         Description = description,
    //         SequenceNumber = nextSequenceNumber
    //     };
    //
    //     // Add and save to the database
    //     _context.Tasks.Add(task);
    //     await _context.SaveChangesAsync();
    //
    //     return task;
    // }
    
    
    
    //example + Order no 
    //ORDER NO is double so when creating a new task can choose after which task id/order so then it will calculate the midpoint between the selected and next one.

    // public async Task<Task> AddTaskAfterAsync(int projectId, int afterTaskId, string description)
    // {
    //     // Retrieve the project and relevant tasks
    //     var project = await _context.Projects
    //         .Include(p => p.Tasks)
    //         .FirstOrDefaultAsync(p => p.ProjectId == projectId);
    //
    //     if (project == null)
    //         throw new Exception("Project not found");
    //
    //     // Find the target task and the next task in order
    //     var afterTask = project.Tasks.FirstOrDefault(t => t.TaskId == afterTaskId);
    //     var nextTask = project.Tasks
    //         .Where(t => t.Order > afterTask.Order)
    //         .OrderBy(t => t.Order)
    //         .FirstOrDefault();
    //
    //     // Calculate new order as midpoint between `afterTask` and `nextTask`
    //     double newOrder = nextTask != null
    //         ? (afterTask.Order + nextTask.Order) / 2
    //         : afterTask.Order + 1; // If no next task, place at the end
    //
    //     // Create and add the new task
    //     var newTask = new Task
    //     {
    //         ProjectId = projectId,
    //         Description = description,
    //         SequenceNumber = project.Tasks.Any() ? project.Tasks.Max(t => t.SequenceNumber) + 1 : 1,
    //         Order = newOrder
    //     };
    //
    //     _context.Tasks.Add(newTask);
    //     await _context.SaveChangesAsync();
    //
    //     return newTask;
    // }
  
