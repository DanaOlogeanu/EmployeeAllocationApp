using Domain.Dtos;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserSkillDao
{
    //user skill
    Task<UserSkill> CreateAsync(UserSkill skill);
    //return list of skills according to username, skill name or proficiency
    Task<IEnumerable<UserSkill>> GetAsync(SearchUserSkillParametersDto searchParameters);
    
    
    //list of user skills 
    Task<IEnumerable<UserSkill>> GetUserSkills(string username);
   
    //view single user skill
    Task <UserSkill?> GetByIdAsync(int id);
}