using Application.DaoInterfaces;
using Application.LogicInterfaces;

namespace Application.Logic;

public class DepartmentLogic:IDepartmentLogic
{
    private readonly IDepartmentDao dptDao;
  

    public DepartmentLogic(IDepartmentDao dptDao)
    {
        this.dptDao = dptDao;
    }
    
    
    public async Task<List<string>> GetUniqueDepartmentsAsync()
    {
        List<string> dpts = await dptDao.GetUniqueDepartmentsAsync();
        if (dpts == null)
        {
            throw new Exception($"Departments not found");
        }
        return dpts;
    }
}