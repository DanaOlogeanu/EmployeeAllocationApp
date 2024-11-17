using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class TasksApprovalsController : ControllerBase
{
    private readonly ITaskApprovalLogic taskApprovalLogic;
    
    public TasksApprovalsController(ITaskApprovalLogic taskApprovalLogic)
    {
        this.taskApprovalLogic = taskApprovalLogic;
    }

    [HttpPost]
    public async Task<ActionResult<TaskApproval>> CreateAsync([FromBody]TaskApprovalCreationDto dto)
    {
        try
        {
            TaskApproval created = await taskApprovalLogic.CreateAsync(dto);
            return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet ("viewApprovals")]
    public async Task<ActionResult<IEnumerable<TaskApproval>>> GetApprovalsManager([FromQuery] string username)
    {
        try
        {
            IEnumerable<TaskApproval> tasksApprovals = await taskApprovalLogic.GetApprovalsManager(username);
            return Ok (tasksApprovals);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] TaskApprovalBasicDto dto)
    {
        try
        {
            await taskApprovalLogic.UpdateAsync(dto); 
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskApprovalBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            TaskApprovalBasicDto result = await taskApprovalLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet ("noPending") ]
    public async Task<ActionResult<int>> GetPendingApprovalAsync([FromQuery] string username)
    {
        try
        {
            int pendingCount = await taskApprovalLogic.GetPendingApprovalAsync(username);
            return Ok(pendingCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    
}