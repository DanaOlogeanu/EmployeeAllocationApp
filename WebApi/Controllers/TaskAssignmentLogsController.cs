using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class TaskAssignmentLogsController: ControllerBase
{
    private readonly ITaskAssignmentLogLogic assignLogLogic;

    public TaskAssignmentLogsController(ITaskAssignmentLogLogic assignLogLogic)
    {
        this.assignLogLogic = assignLogLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<TaskAssignmentLog>> CreateAsync([FromBody]TaskAssignmentLogCreationDto dto)
    {
        try
        {
            TaskAssignmentLog created = await assignLogLogic.CreateAsync(dto);
            return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpGet("taskProject/{taskProjectId:int}")]
    public async Task<ActionResult<List<TaskAssignmentLogBasicDto>>> GetTaskAssignmentLogsForProject([FromRoute] int taskProjectId)
    {
        try
        {
            List<TaskAssignmentLogBasicDto> result = await assignLogLogic.GetTaskAssignmentLogsForProject(taskProjectId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}