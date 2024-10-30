using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class SkillLogic: ISkillLogic
{
    private readonly ISkillDao skillDao;
  

    public SkillLogic(ISkillDao skillDao)
    {
        this.skillDao = skillDao;
    }

    public async Task<List<string>> GetUniqueCategoriesAsync()
    {
        List<string> categories = await skillDao.GetUniqueCategoriesAsync();
        if (categories == null)
        {
            throw new Exception($"Skill categories not found");
        }

        return categories;

    }

    public async Task<List<string>> GetUniqueSkillsAsync(string cat)
    {
        List<string> skills = await skillDao.GetUniqueSkillsAsync(cat);
        if (skills == null)
        {
            throw new Exception($"Skills by category not found");
        }

        return skills;

    }
    
    //return all users with a specific skill? 
    public async Task<SkillBasicDto> GetByNameAsync(string skillName)
    {
        Skill? skill = await skillDao.GetByNameAsync(skillName);
        if (skill == null)
        {
            throw new Exception($"Skill with name {skillName} not found");
        }

        return new SkillBasicDto (skill.Name, skill.Category);
    }
}