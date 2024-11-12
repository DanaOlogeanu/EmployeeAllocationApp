using Domain.Dtos;
using Domain.Models;

namespace HttpClients.ClientInterfaces
{
    public interface ITaskSkillService
    {
        Task<TaskSkill> CreateAsync(TaskSkillCreationDto dto);
        Task<ICollection<TaskSkill>> GetAsync(string? skillName, int? proficiency, int? taskProjectId);
        Task<TaskSkillBasicDto> GetByIdAsync(int id);
        Task UpdateAsync(TaskSkillBasicDto taskSkill);
        Task DeleteAsync(int taskSkillId);
    }
}