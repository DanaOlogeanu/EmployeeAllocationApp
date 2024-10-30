using Domain.Dtos;

namespace Application.LogicInterfaces;

public interface ISkillLogic
{
    Task<SkillBasicDto> GetByNameAsync(string skillName);
    Task<List<string>> GetUniqueCategoriesAsync();
    Task<List<string>> GetUniqueSkillsAsync(string cat);
}