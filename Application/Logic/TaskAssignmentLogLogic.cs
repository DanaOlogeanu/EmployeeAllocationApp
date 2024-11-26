using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class TaskAssignmentLogLogic : ITaskAssignmentLogLogic
{
    private readonly ITaskProjectDao taskProjectDao;
    private readonly ITaskAssignmentLogDao assignLogDao;
    private readonly IUserDao userDao;
    private readonly IProjectDao projectDao;

    public TaskAssignmentLogLogic(ITaskProjectDao taskProjectDao, ITaskAssignmentLogDao assignLogDao,IUserDao userDao, IProjectDao projectDao)
    {
        this.taskProjectDao = taskProjectDao;
        this.assignLogDao = assignLogDao;
        this.userDao = userDao;
        this.projectDao = projectDao;
    }

    public async Task<TaskAssignmentLog> CreateAsync(TaskAssignmentLogCreationDto creationDto)
    {
        TaskProject? existing = await taskProjectDao.GetByIdAsync(creationDto.TaskProjectId);
        if (existing == null)
        {
            throw new Exception("Project not found");
        }

        User? user = await userDao.GetByUsernameAsync(creationDto.AssignedBy);
        if (user == null)
        {
            throw new Exception("User not found - Assigned by");
        }
        ValidateData(creationDto);
        TaskAssignmentLog toCreate = new TaskAssignmentLog(existing.Id, user.Username, existing.OwnerUsername);
        TaskAssignmentLog created = await assignLogDao.CreateAsync(toCreate);

        return created;
    }

    private void ValidateData(TaskAssignmentLogCreationDto creationDto)
    {
        if (string.IsNullOrEmpty(creationDto.AssignedBy)) throw new Exception("Assigned by cannot be empty.");
        if ((int)(creationDto.TaskProjectId) == 0) throw new Exception(" Task project id cannot be empty.");
        if (string.IsNullOrEmpty(creationDto.AssignedTo)) throw new Exception("Assigned to cannot be empty.");
    }

    public async Task<List<TaskAssignmentLogBasicDto>> GetTaskAssignmentLogsForProject(int taskId)
    {
        List<TaskAssignmentLog>? logs = await assignLogDao.GetTaskAssignmentLogsForProject(taskId);
        
        List<TaskAssignmentLogBasicDto> logForProjectTasks = new List<TaskAssignmentLogBasicDto>();
        foreach (var log in logs)
        {
            var result = new TaskAssignmentLogBasicDto(log.Id,log.TaskProjectId, log.AssignedBy, log.AssignedTo,log.Date);
            logForProjectTasks.Add(result);
        }

        return logForProjectTasks;
    }
    
}