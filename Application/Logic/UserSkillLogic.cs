using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class UserSkillLogic:IUserSkillLogic
{
    private readonly IUserSkillDao userSkillDao;
    private readonly IUserDao userDao;
    private readonly ISkillDao skillDao;

    public UserSkillLogic(IUserSkillDao userSkillDao, IUserDao userDao,ISkillDao skillDao)
    {
        this.userSkillDao = userSkillDao;
        this.userDao = userDao;
        this.skillDao = skillDao;
    }
    
    public async Task<UserSkill> CreateAsync(UserSkillCreationDto dto)
    {
          
        User? user = await userDao.GetByUsernameAsync(dto.Username);
        
        if (user == null)
        {
            throw new Exception($"User with username {dto.Username} was not found.");
        }
        Skill? skill = await skillDao.GetByNameAsync(dto.SkillName);
        
        if (skill == null)
        {
            throw new Exception($"Skill with name {dto.SkillName} was not found.");
        }
            ValidateData(dto);
            UserSkill toCreate = new UserSkill(user.Username, dto.SkillName, dto.Proficiency, dto.Notes);
            UserSkill created = await userSkillDao.CreateAsync(toCreate);
    
            return created;
        }



    private void ValidateData(UserSkillCreationDto dto)
    {
        
        if ((int)(dto.Proficiency)==0) throw new Exception("Skill proficiency cannot be empty."); 
    //    if (string.IsNullOrEmpty(dto.SkillName)) throw new Exception("Skill name cannot be empty.");
        
        // if((int)dto.SubCategoryId==0)   throw new Exception($"Product subcategory was not found.");
        // // if (string.IsNullOrEmpty(dto.SubCategoryName)) throw new Exception("Product subcategory cannot be empty.");
        // if (string.IsNullOrEmpty(dto.Type)) throw new Exception("Product Type cannot be empty.");
        // if (string.IsNullOrEmpty(dto.Brand)) throw new Exception("Product Brand cannot be empty.");
        // if ((int)(dto.Qty)==0) throw new Exception("Product quantity cannot be empty."); 
        // other validation stuff
    }
    
    //return list according to username. skillname or proficiency
    
    public Task<IEnumerable<UserSkill>> GetAsync(SearchUserSkillParametersDto searchParameters)
    {
        return userSkillDao.GetAsync(searchParameters);
    }
    
    
    
    //return list of skills for one user
    public Task<IEnumerable<UserSkill>> GetUserSkills(string username)
    {
        return userSkillDao.GetUserSkills(username);
    }
    
    //return 1 user skill by userskillId
    public async Task<UserSkillBasicDto> GetByIdAsync(int id)
    {
        UserSkill? skill = await userSkillDao.GetByIdAsync(id);
        if (skill == null)
        {
            throw new Exception($"User skill with id {id} not found");
        }

        return new UserSkillBasicDto (skill.UserSkillId, skill.Owner.Username, skill.Skill.Name,skill.Proficiency, skill.Notes);
    }

 
}