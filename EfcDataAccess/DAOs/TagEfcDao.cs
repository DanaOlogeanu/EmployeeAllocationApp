using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess.DAOs;

public class TagEfcDao:ITagDao
{
    private readonly AppContext context;

    public TagEfcDao(AppContext context)
    {
        this.context = context;
    }

    public async Task<List<string>> GetUniqueCategoriesAsync()
    {
        return await context.Tags
            .Select(t => t.Category)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<string>> GetTagsAsync(string cat)
    {
        return await context.Tags
            .Where(s => s.Category == cat) 
            .Select(s => s.Name)
            .Distinct()
            .ToListAsync();
    }
}