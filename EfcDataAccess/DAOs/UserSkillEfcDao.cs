using Application.DaoInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserSkillEfcDao:IUserSkillDao

{
    private readonly AppContext context;

    public UserSkillEfcDao(AppContext context)
    {
        this.context = context;
    }
    
    public async Task<UserSkill> CreateAsync(UserSkill skill)
    {
        try
        {
        EntityEntry<UserSkill> added = await context.UserSkills.AddAsync(skill);
        await context.SaveChangesAsync();
        return added.Entity;
        }
        catch (DbUpdateException dbEx)  // Catch EF Core-specific exceptions
        {
            // Print or log the inner exception
            Console.WriteLine($"Error: {dbEx.Message}");
            if (dbEx.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {dbEx.InnerException.Message}");
            }

            // Re-throw or handle the exception
            throw;
        }
        catch (Exception ex)  // Catch any general exception
        {
            Console.WriteLine($"General error: {ex.Message}");
            throw;
        }
    }

    //TODO UPDATE REVIEW
    //list of user skills according to username, user skill or proficiency
    public async Task<IEnumerable<UserSkill>> GetAsync(SearchUserSkillParametersDto searchParameters)
    {
        IQueryable<UserSkill> query = context.UserSkills.Include(us => us.Owner).Include(us=>us.Skill).AsQueryable();
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(us =>
                us.Owner.Username.ToLower().Equals(searchParameters.Username.ToLower()));
        }
        if (!string.IsNullOrEmpty(searchParameters.SkillName))
        {
          
            query = query.Where(us =>
                us.Skill.Name.ToLower().Equals(searchParameters.SkillName.ToLower()));
        }
        if (searchParameters.Proficiency != null)
        {
            query = query.Where(us => us.Proficiency == searchParameters.Proficiency);
        }
        List<UserSkill> result = await query.ToListAsync();
        return result;
    }

    //get list of skills for a user 
    public async Task<IEnumerable<UserSkill>> GetUserSkills(string username)
    {
        IQueryable<UserSkill> skillsQuery = context.UserSkills.Include (us => us.Owner).AsQueryable();
        if (!string.IsNullOrEmpty(username))
        {
            skillsQuery = skillsQuery.Where(u => u.Owner.Username == username);
        }
   
        List<UserSkill> result = await skillsQuery.ToListAsync();
        return result; 
    }

    //view single user skill
    public async Task<UserSkill?> GetByIdAsync(int id)
    {
        UserSkill? found = await context.UserSkills
          //  .AsNoTracking()
            .Include(us => us.Owner) 
            .Include (us => us.Skill)  
            .SingleOrDefaultAsync(us=> us.UserSkillId == id);
        return found;
    }
    
    //update skill 
    public async Task UpdateAsync(UserSkillBasicDto userSkill)
    {
        var existingSkill = await context.UserSkills.FindAsync(userSkill.UserSkillId);
        if (existingSkill == null)
        {
            throw new Exception("User skill not found");
        }

        existingSkill.Proficiency = userSkill.Proficiency;
        existingSkill.Notes = userSkill.Notes;

        context.UserSkills.Update(existingSkill);
        await context.SaveChangesAsync();
    }
    
    //delete skill
    public async Task DeleteAsync(int userSkillId)
    {
        var existingSkill = await context.UserSkills.FindAsync(userSkillId);
        if (existingSkill == null)
        {
            throw new Exception("User skill not found");
        }

        context.UserSkills.Remove(existingSkill);
        await context.SaveChangesAsync();
    }
    
}