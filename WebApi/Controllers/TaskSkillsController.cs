using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TaskSkillsController : ControllerBase
{
    private readonly ITaskSkillLogic taskSkillLogic;

    public TaskSkillsController(ITaskSkillLogic taskSkillLogic)
    {
        this.taskSkillLogic = taskSkillLogic;
    }

    [HttpPost]
    public async Task<ActionResult<TaskSkill>> CreateAsync([FromBody] TaskSkillCreationDto dto)
    {
        try
        {
            TaskSkill created = await taskSkillLogic.CreateAsync(dto);
            return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskSkill>>> GetAsync([FromQuery] int? taskProjectId, [FromQuery] string? skillName, [FromQuery] Proficiency? proficiency)
    {
        try
        {
            SearchTaskSkillParametersDto parameters = new(taskProjectId, skillName, proficiency);
            var taskSkills = await taskSkillLogic.GetAsync(parameters);
            return Ok(taskSkills);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskSkillBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            TaskSkillBasicDto result = await taskSkillLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] TaskSkillBasicDto taskSkill)
    {
        try
        {
            if (id != taskSkill.TaskSkillId)
            {
                return BadRequest("ID mismatch");
            }

            await taskSkillLogic.UpdateAsync(taskSkill);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            await taskSkillLogic.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
