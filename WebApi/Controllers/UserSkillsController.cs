using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class UserSkillsController : ControllerBase
{
    private readonly IUserSkillLogic userSkillLogic;

    public UserSkillsController(IUserSkillLogic userSkillLogic)
    {
        this.userSkillLogic = userSkillLogic;
    }
    
      
    //endpoint
    //create user skill
    [HttpPost]
    public async Task<ActionResult<UserSkill>> CreateAsync([FromBody]UserSkillCreationDto dto)
    {
        try
        {
            UserSkill created = await userSkillLogic.CreateAsync(dto);
             // return Created($"/OneUserSkill/{created.UserSkillId}", created); 
             return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //TODO Review 
    //list of userskills from username.skill name or proficiency
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserSkill>>> GetAsync([FromQuery] string? userName, [FromQuery] string? skillName,[FromQuery] Proficiency? proficiency)
    {
        try
        {
            SearchUserSkillParametersDto parameters = new(userName, skillName, proficiency);
            var todos = await userSkillLogic.GetAsync(parameters);
            return Ok(todos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    //GET -return a single user skill 
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserSkillBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            UserSkillBasicDto result = await userSkillLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    //get list of skills for a user 
    [HttpGet ("viewUserSkills")]
    public async Task<ActionResult<IEnumerable<UserSkill>>> GetUserSkills([FromQuery] string username)
    {
        try
        {
            IEnumerable<UserSkill> userSkills = await userSkillLogic.GetUserSkills(username);
            return Ok (userSkills);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //Update skill
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UserSkillBasicDto userSkill)
    {
        try
        {
            if (id != userSkill.UserSkillId)
            {
                return BadRequest("ID mismatch");
            }

            await userSkillLogic.UpdateAsync(userSkill);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //Delete skill 
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            await userSkillLogic.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}