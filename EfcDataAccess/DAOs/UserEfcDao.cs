using Application.DaoInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess.DAOs;

public class UserEfcDao:IUserDao

{
    
    private readonly AppContext context;

    public UserEfcDao(AppContext context)
    {
        this.context = context;
    }
    
    public async Task<User?> GetByUsernameAsync(string userName)
    {
        // User? user = await context.Users.FindAsync(id);   //takes type int
        // return user;

        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }
    
    
    //TODO: UPDATE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IQueryable<User> usersQuery = context.Users.Include(user => user.UserSkills).ThenInclude(us=>us.Skill ).AsQueryable();
        if (!string.IsNullOrEmpty(searchParameters.Department))
        {
            usersQuery = usersQuery.Where(u => u.Department.Name.ToLower().Contains(searchParameters.Department.ToLower()));
        }
        if (!string.IsNullOrEmpty(searchParameters.SkillOne))
        {
            usersQuery = usersQuery.Where(u => u.UserSkills.Any(us =>
                us.Skill.Name.Equals(searchParameters.SkillOne.ToLower()) &&
                (us.Proficiency >= searchParameters.ReqScoreOne)));
        }
        IEnumerable<User> result = await usersQuery.ToListAsync();
        return result;
    }
    
    
}