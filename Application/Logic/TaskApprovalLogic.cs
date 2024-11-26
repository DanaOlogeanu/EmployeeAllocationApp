using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class TaskApprovalLogic:ITaskApprovalLogic
{
    private readonly ITaskApprovalDao approvalDao;
    private readonly ITaskProjectDao taskProjectDao;

    public TaskApprovalLogic(ITaskApprovalDao approvalDao,ITaskProjectDao taskProjectDao )
    {
        this.approvalDao = approvalDao;
        this.taskProjectDao = taskProjectDao;

    }

    public async Task<TaskApproval> CreateAsync(TaskApprovalCreationDto dto)
    {
        // TaskProject? p = await taskProjectDao.GetByIdAsync(dto.TaskProjectId);
        //
        // if (p == null)
        // {
        //     throw new Exception($"Task with id {dto.TaskProjectId} was not found.");
        // }

        ValidateData(dto);
        TaskApproval toCreate = new TaskApproval(dto.TaskProjectId, dto.OwnerUsername, dto.Status);
        TaskApproval created = await approvalDao.CreateAsync(toCreate);

        return created;
    }

    public Task<IEnumerable<TaskApproval>> GetApprovalsManager(string username)
    {
        return approvalDao.GetApprovalsManager(username);
    }

    
    public async Task UpdateAsync(TaskApprovalBasicDto dto)
    {
        TaskApproval? existing = await approvalDao.GetByIdAsync(dto.TaskApprovalId);
        if (existing == null)
        {
            throw new Exception("Task approval not found");
        }
        // existing.Id = taskProject.Id;
        existing.Status = (ApprovalStatus)dto.Status;
        existing.Comments = dto.Comments;
     
        await approvalDao.UpdateAsync(existing);
    }

    public async Task<TaskApprovalBasicDto?> GetByIdAsync(int id)
    {
        TaskApproval? taskApproval = await approvalDao.GetByIdAsync(id);
        if ( taskApproval == null)
        {
            throw new Exception($"Approval with id {id} not found");
        }

        return new TaskApprovalBasicDto ( taskApproval.Id, taskApproval.TaskProjectId,taskApproval.OwnerUsername, taskApproval.Status, taskApproval.Comments, taskApproval.Date);
    }

   public async Task<List<TaskApprovalBasicDto>> GetByTaskIdAsync(int taskProjectId)
    {
        List<TaskApprovalBasicDto> result= new List<TaskApprovalBasicDto>();
        List<TaskApproval> approvals =await approvalDao.GetByTaskIdAsync(taskProjectId);
        foreach (TaskApproval approval in approvals)
        {
            var p = new TaskApprovalBasicDto ( approval.Id, approval.TaskProjectId,approval.OwnerUsername, approval.Status, approval.Comments, approval.Date);
            result.Add(p);
        }
        return result;
    }
   
    public Task<int> GetPendingApprovalAsync(string username)
    {
        return approvalDao.GetPendingApprovalAsync(username);
    }


    private void ValidateData(TaskApprovalCreationDto dto)
    {
        
        if (string.IsNullOrEmpty(dto.OwnerUsername))
            throw new Exception("Owner username cannot be null or empty.");

        if (dto.TaskProjectId <= 0)
            throw new Exception("Invalid TaskProjectId.");
        
    }
    
    
}