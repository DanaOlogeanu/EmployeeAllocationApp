namespace EfcDataAccess.DAOs;

public class TaskProjectEfcDao
{
    
    
    
    
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
}