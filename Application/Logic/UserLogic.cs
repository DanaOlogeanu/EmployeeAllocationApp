using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class UserLogic: IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
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
}