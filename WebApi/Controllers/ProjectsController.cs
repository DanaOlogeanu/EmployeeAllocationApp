using Application.Logic;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class ProjectsController : ControllerBase
{
    private readonly IProjectLogic projectLogic;

    public ProjectsController(IProjectLogic projectLogic)
    {
        this.projectLogic = projectLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Project>> CreateAsync([FromBody]ProjectCreationDto dto)
    {
        try
        {
            Project created = await projectLogic.CreateAsync(dto);
            return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            ProjectBasicDto result = await projectLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] ProjectBasicDto dto)
    {
        try
        {
            await projectLogic.UpdateAsync(dto); 
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet ("viewProjects")]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects([FromQuery] string username)
    {
        try
        {
            IEnumerable<Project> projects = await projectLogic.GetProjects(username);
            return Ok (projects);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    
    
        [HttpGet("SearchProjects")]
        public async Task<ActionResult<IEnumerable<Project>>> SearchProjects([FromQuery] SearchProjectParameters parameters)
        {
            var projects = await projectLogic.SearchProjectsAsync(parameters);
            if (projects == null || !projects.Any())
            {
                return Ok(new List<Project>()); // Return an empty list instead of NotFound
            }
            return Ok(projects);
        }
    }

       


    


    
