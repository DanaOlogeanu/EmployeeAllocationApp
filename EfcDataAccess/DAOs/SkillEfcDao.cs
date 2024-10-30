using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess.DAOs;

public class SkillEfcDao:ISkillDao
{
    private readonly AppContext context;

    public SkillEfcDao(AppContext context)
    {
        this.context = context;
    }

    //get skill by name
    public async Task<Skill?> GetByNameAsync(string name)
    {
        Skill? existing = await context.Skills.FirstOrDefaultAsync(u =>
            u.Name.ToLower().Equals(name.ToLower())
        );
        return existing;
    }
    
    public async Task<List<string>> GetUniqueCategoriesAsync()
    {
        return await context.Skills
            .Select(s => s.Category)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<string>> GetUniqueSkillsAsync(string cat)
    {
    return await context.Skills
        .Where(s => s.Category == cat) 
        .Select(s => s.Name)
        .Distinct()
        .ToListAsync();
    }
}