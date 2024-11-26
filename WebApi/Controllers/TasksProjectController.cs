using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class TasksProjectController : ControllerBase
    {
    private readonly ITaskProjectLogic taskProjectLogic;
    
    public TasksProjectController(ITaskProjectLogic taskProjectLogic)
    {
        this.taskProjectLogic = taskProjectLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<TaskProject>> CreateAsync([FromBody]TaskProjectCreationDto dto)
    {
        try
        {
            TaskProject created = await taskProjectLogic.CreateAsync(dto);
            return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskProjectBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            TaskProjectBasicDto result = await taskProjectLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] TaskProjectBasicDto dto)
    {
        try
        {
            await taskProjectLogic.UpdateAsync(dto); 
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet ("viewTasks")]
    public async Task<ActionResult<IEnumerable<TaskProject>>> GetTasksUser([FromQuery] string username)
    {
        try
        {
            IEnumerable<TaskProject> tasksProject = await taskProjectLogic.GetTasksUser(username);
            return Ok (tasksProject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("SearchTasks")]
    public async Task<ActionResult<IEnumerable<TaskProject>>> SearchTasksAsync([FromQuery] SearchTaskProjectParametersDto parameters)
    {
        try
        {
            IEnumerable<TaskProject> tasks = await taskProjectLogic.SearchTasksAsync(parameters);
            return Ok(tasks);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}