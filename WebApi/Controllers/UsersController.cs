using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")] //"route template", the URI will be localhost:port/users
public class UsersController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UsersController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    
    
    // TODO:  REVIEW SEARCH PARAMETERS
    // /GET request to  users list   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? department,[FromQuery] string?  skillOne, [FromQuery] Proficiency? reqScoreOne,[FromQuery] string? skillTwo, [FromQuery] Proficiency? reqScoreTwo,[FromQuery]  string? skillThree, [FromQuery] Proficiency? reqScoreThree)
    {
        try
        {
            SearchUserParametersDto parameters = new (department, skillOne, reqScoreOne,  skillTwo,  reqScoreTwo, skillThree, reqScoreThree);
            var users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    } 
    
    
}