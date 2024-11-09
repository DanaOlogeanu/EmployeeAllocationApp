using Domain.Dtos;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserSkillLogic
{
    //create user skill
    Task<UserSkill> CreateAsync(UserSkillCreationDto dto);
    //return list of skills according to username, skill name or proficiency
    Task<IEnumerable<UserSkill>> GetAsync(SearchUserSkillParametersDto searchParameters);
    //  //return list of skills for one user
    Task<IEnumerable<UserSkill>> GetUserSkills(string username);
    // view single user skill
    Task<UserSkillBasicDto> GetByIdAsync(int id);
    //update skill
    Task UpdateAsync(UserSkillBasicDto userSkill);
    //delete skill
    Task DeleteAsync(int userSkillId);
 
   
}