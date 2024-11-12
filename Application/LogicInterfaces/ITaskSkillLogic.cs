using Domain.Dtos;
using Domain.Models;

namespace Application.LogicInterfaces
{
    public interface ITaskSkillLogic
    {
        Task<TaskSkill> CreateAsync(TaskSkillCreationDto dto);
        Task<IEnumerable<TaskSkill>> GetAsync(SearchTaskSkillParametersDto searchParameters);
        Task<TaskSkillBasicDto> GetByIdAsync(int id);
        Task UpdateAsync(TaskSkillBasicDto taskSkill);
        Task DeleteAsync(int taskSkillId);
    }
}