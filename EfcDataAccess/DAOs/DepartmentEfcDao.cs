using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess.DAOs;

public class DepartmentEfcDao:IDepartmentDao
    {
    private readonly AppContext context;

    public DepartmentEfcDao(AppContext context)
    {
        this.context = context;
    }

    public async Task<List<string>> GetUniqueDepartmentsAsync()
    {
        return await context.Departments
            .Select(d => d.Name)
            .Distinct()
            .ToListAsync();  
    }
    }