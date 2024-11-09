using Domain.Dtos;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserSkillService
{
    //create user skill
    Task<UserSkill> CreateAsync(UserSkillCreationDto dto);
    //list of user skills according to username, skill name or proficiency
    Task<ICollection<UserSkill>> GetAsync(string? userName, string? skillName, int? proficiency);
    
    //  //return list of skills for one user
    Task<IEnumerable<UserSkill>> GetUserSkills(string username);
    // view single skill
    Task<UserSkillBasicDto> GetByIdAsync(int id);
    // update skill
    Task UpdateAsync(UserSkillBasicDto userSkill);
    //delete skill
    Task DeleteAsync(int userSkillId);
}