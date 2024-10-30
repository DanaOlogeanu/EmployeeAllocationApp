using Application.LogicInterfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


    [ApiController]
    [Route("[controller]")]

    public class SkillsController : ControllerBase
    {
        private readonly ISkillLogic skillLogic;

        public SkillsController(ISkillLogic skillLogic)
        {
            this.skillLogic = skillLogic;
        }
        
        
        
        [HttpGet ("skill")]
        public async Task<ActionResult<SkillBasicDto>> GetByNameAsync([FromQuery] string username)
        {
            try
            {
                SkillBasicDto result = await skillLogic.GetByNameAsync(username);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetUniqueCategories()
        {
            try
            {
                List<string> categories = await skillLogic.GetUniqueCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet ("skillByCat")]
        public async Task<ActionResult<List<string>>> GetUniqueSkills([FromQuery]string category)  //from query same as in @something
        {
            try
            {
                List<string> skills = await skillLogic.GetUniqueSkillsAsync(category);
                return Ok(skills);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

    }