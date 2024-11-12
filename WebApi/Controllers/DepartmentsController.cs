using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class DepartmentsController: ControllerBase
{
    private readonly IDepartmentLogic dptLogic;

    public DepartmentsController(IDepartmentLogic dptLogic)
    {
        this.dptLogic = dptLogic;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetUniqueDepartments()
    {
        try
        {
            List<string> dpts = await dptLogic.GetUniqueDepartmentsAsync();
            return Ok(dpts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}