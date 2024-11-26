using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class UserLogic: IUserLogic
{
    private readonly IUserDao userDao;
    private readonly IUserSkillDao userSkillDao;

    public UserLogic(IUserDao userDao, IUserSkillDao userSkillDao) 
    { 
        this.userDao = userDao; 
        this.userSkillDao = userSkillDao; 
    }
    
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters); //returns a Task, but we don't need to await it, because we do not need the result here. Instead, we actually just returns that task, to be awaited somewhere else.
    }
    
    
    //log in
    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await userDao.GetByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;  
    }
    
    //availability 
    public Task<DateOnly> SoonestAvailabilityForUser(string username)
    {
        return userDao.SoonestAvailabilityForUser(username);
    }

  public async Task<User?> GetByUsernameAsync(string username)
    {
        User? existingUser = await userDao.GetByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
        return existingUser;  
    }

  public async Task<IEnumerable<User>?> GetByDepartmentAsync(string selectedDpt)
  {
     IEnumerable<User>? existingUsers = await userDao.GetByDepartmentAsync(selectedDpt);
        
     if (existingUsers == null)
     {
         throw new Exception("Users not found for the selected department");
     }
     return existingUsers;  
  }

  public async Task<bool> IsOnHoliday(string username, DateOnly date)
  {
      return await userDao.IsOnHoliday(username, date);
  }
  public async Task<DepartmentMatrixDto> GetUsersByDepartmentAsync(string departmentName)
  {
      var users = await userDao.GetByDepartmentAsync(departmentName);

      if (users == null || !users.Any())
      {
          throw new Exception("Users not found for the selected department");
      }

      var userDtos = users.Select(user => new UserMatrixDto(user.Username, user.Name)).ToList();
      var department = new DepartmentMatrixDto(departmentName, userDtos);

      foreach (var user in department.Users)
      {
          var userSkills = await GetUserSkills(user.Username);
          if (userSkills == null)
          {
              Console.WriteLine($"UserSkills for {user.Username} is null.");
              continue;
          }
          user.Skills = userSkills.Select(us => new UserSkillMatrixDto(us.Skill.Name, us.Proficiency)).ToList();
      }

      return department;
  }

  public Task<IEnumerable<UserSkill>> GetUserSkills(string username)
  {
      return userSkillDao.GetUserSkills(username);
  }
  
}