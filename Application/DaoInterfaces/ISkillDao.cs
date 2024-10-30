using Domain.Models;

namespace Application.DaoInterfaces;

public interface ISkillDao
{
    Task<Skill?> GetByNameAsync(String name);
    Task<List<string>> GetUniqueCategoriesAsync();
    Task<List<string>> GetUniqueSkillsAsync(string cat);
}