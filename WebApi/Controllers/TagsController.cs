using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class TagsController: ControllerBase
{
    private readonly ITagLogic tagLogic;

    public TagsController(ITagLogic tagLogic)
    {
        this.tagLogic = tagLogic;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetUniqueCategories()
    {
        try
        {
            List<string> categories = await tagLogic.GetUniqueCategoriesAsync();
            return Ok(categories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet ("tagByCat")]
    public async Task<ActionResult<List<string>>> GetUniqueSkills([FromQuery]string category)  //from query same as in @something
    {
        try
        {
            List<string> tags = await tagLogic.GetTagsAsync(category);
            return Ok(tags);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}