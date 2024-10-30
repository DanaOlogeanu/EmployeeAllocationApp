using Domain.Dtos;

namespace HttpClients.ClientInterfaces;

public interface ISkillService
{
    Task<SkillBasicDto> GetByNameAsync(string name);
    Task<List<string>> GetUniqueCategories();
    Task<List<string>> GetUniqueSkills(string? cat);
}