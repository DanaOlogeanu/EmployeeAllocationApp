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
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? department,
        [FromQuery] string? skillOne, [FromQuery] Proficiency? reqScoreOne, [FromQuery] string? skillTwo,
        [FromQuery] Proficiency? reqScoreTwo, [FromQuery] string? skillThree, [FromQuery] Proficiency? reqScoreThree)
    {
        try
        {
            SearchUserParametersDto parameters = new(department, skillOne, reqScoreOne, skillTwo, reqScoreTwo,
                skillThree, reqScoreThree);
            var users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("availability")]
    public async Task<ActionResult<DateOnly>> SoonestAvailabilityForUser([FromQuery] string username)
    {
        try
        {
            DateOnly date = await userLogic.SoonestAvailabilityForUser(username);
            return Ok(date);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }



    [HttpGet("getUser")]
    public async Task<ActionResult<User?>> GetByUsernameAsync([FromQuery]string username)
    {
        try
        {
            User? user = await userLogic.GetByUsernameAsync(username);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpGet("getByDpt")]
 public async Task<ActionResult<IEnumerable<User>?>> GetByDepartmentAsync([FromQuery]string selectedDpt)
    {
        try
        {
            IEnumerable<User>? users = await userLogic.GetByDepartmentAsync(selectedDpt);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

 // New method to get department matrix
 [HttpGet("getDepartmentMatrix")]
 public async Task<ActionResult<DepartmentMatrixDto>> GetUsersByDepartmentMatrixAsync([FromQuery]string selectedDpt)
 {
     try
     {
         if (string.IsNullOrEmpty(selectedDpt))
         {
             return BadRequest("Department name cannot be null or empty");
         }

         var department = await userLogic.GetUsersByDepartmentAsync(selectedDpt);
         return Ok(department);
     }
     catch (Exception e)
     {
         Console.WriteLine(e);
         return StatusCode(500, e.Message);
     }
 }
 
 
[HttpGet("getHoliday")]    ///????
        public async Task<ActionResult<Task<bool>>> IsOnHoliday([FromQuery]string username, [FromQuery]DateOnly date)
        {
            try
            {
                bool response =  await userLogic.IsOnHoliday(username,date);
                return (Ok (response));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return(StatusCode(500, e.Message));
            }  
        }
    
}