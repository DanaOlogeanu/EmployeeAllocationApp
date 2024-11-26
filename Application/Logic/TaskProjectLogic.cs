using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class TaskProjectLogic:ITaskProjectLogic
{
    private readonly IProjectDao projectDao;
    private readonly ITaskProjectDao taskProjectDao;
    private readonly IUserDao userDao;
    
    public TaskProjectLogic(IProjectDao projectDao,ITaskProjectDao taskProjectDao, IUserDao userDao)
    {
        this.projectDao = projectDao;
        this.taskProjectDao = taskProjectDao;
        this.userDao = userDao;
    }

    
    
    public async Task<TaskProject> CreateAsync(TaskProjectCreationDto dto)
    {
        Project? p = await projectDao.GetByIdAsync(dto.ProjectId);

        if (p == null)
        {
            throw new Exception($"Project with id {dto.ProjectId} was not found.");
        }

        //ValidateData(dto);
        TaskProject toCreate = new TaskProject(dto.Name, dto.ProjectId, dto.OwnerUsername, dto.Estimate, dto.TaskStatusEnum, dto.StartDate,
            dto.Deadline, dto.DependentOn, dto.OrderNo, dto.Notes);
        TaskProject created = await taskProjectDao.CreateAsync(toCreate);

        return created;
    }

    public async Task<TaskProjectBasicDto?> GetByIdAsync(int id)
    {
        TaskProject? task = await taskProjectDao.GetByIdAsync(id);
        if ( task == null)
        {
            throw new Exception($"Task with id {id} not found");
        }

        return new TaskProjectBasicDto (task.Id, task.Name, task.ProjectId, task.OwnerUsername,task.Estimate, task.TaskStatusEnum, task.StartDate, task.Deadline,task.DependentOn, task.OrderNo , task.Notes);
    }

    
    public async Task UpdateAsync(TaskProjectBasicDto dto)
    {
        TaskProject? existing = await taskProjectDao.GetByIdAsync(dto.Id);
        if (existing == null)
        {
            throw new Exception("Task not found");
        }
       // existing.Id = taskProject.Id;
        existing.Name = dto.Name;
        existing.OwnerUsername =  dto.OwnerUsername;
        existing.Estimate =  dto.Estimate;
        existing.TaskStatusEnum = dto.TaskStatusEnum;
        existing.StartDate = dto.StartDate;
        existing.Deadline = dto.Deadline;
        existing.DependentOn = dto.DependentOn;
        existing.OrderNo =dto.OrderNo;
         existing.Notes = dto.Notes;
     
        await taskProjectDao.UpdateAsync(existing);
    }

    public Task<IEnumerable<TaskProject>> GetTasksUser(string username)
    {
        return taskProjectDao.GetTasksUser(username);
    }

    public Task<IEnumerable<TaskProject>> SearchTasksAsync(SearchTaskProjectParametersDto parameters)
    {
        return taskProjectDao.SearchTasksAsync(parameters);
    }

    public async Task<TaskProjectBasicDto> GetBySeq(int projectId, int sequenceNo)
    {
        TaskProject? task = await taskProjectDao.GetBySeq(projectId,sequenceNo);
        
        return new TaskProjectBasicDto (task.Id, task.Name, task.ProjectId, task.OwnerUsername,task.Estimate, task.TaskStatusEnum, task.StartDate, task.Deadline,task.DependentOn, task.OrderNo , task.Notes);
    }
}