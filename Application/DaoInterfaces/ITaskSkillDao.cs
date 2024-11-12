using Domain.Dtos;
using Domain.Models;

namespace Application.DaoInterfaces
{
    public interface ITaskSkillDao
    {
        Task<TaskSkill> CreateAsync(TaskSkill taskSkill);
        Task<IEnumerable<TaskSkill>> GetAsync(SearchTaskSkillParametersDto searchParameters);
        Task<TaskSkill?> GetByIdAsync(int id);
        Task UpdateAsync(TaskSkillBasicDto taskSkill);
        Task DeleteAsync(int taskSkillId);
    }
}