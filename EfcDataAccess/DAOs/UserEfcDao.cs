using System.Globalization;
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
        User? existing = await context.Users
            .Include(u => u.Department) // Ensure the Department is included
            .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(userName.ToLower()));
        return existing;
    }


    public async Task<IEnumerable<User>?> GetByDepartmentAsync(string selectedDpt)
    {
        IQueryable<User> usersQuery = context.Users
            .Include(user => user.Department)
            .Include(user=>user.UserSkills)!.ThenInclude(us=>us.Skill )
            .AsQueryable();
        if (!string.IsNullOrEmpty(selectedDpt))
        {
            usersQuery = usersQuery.Where(u => u.Department.Name.ToLower().Equals(selectedDpt.ToLower()));
        }
       
        IEnumerable<User>? result = await usersQuery.ToListAsync();
        foreach (var user in result)
        {
            if (user.UserSkills == null)
            {
                Console.WriteLine($"User {user.Username} has no UserSkills loaded.");
            }
            else
            {
                Console.WriteLine($"User {user.Username} has {user.UserSkills.Count} UserSkills loaded.");
            }
        }
        return result;
    }


    //AVAILABILITY 
    public async Task<List<TaskProject>> GetTasksForUser(string username)
    {
        // Ensure user and their tasks are loaded
        User? user = await context.Users
            .Include(u => u.Tasks) // Eagerly load tasks
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

        // Return ordered tasks or an empty list if user/tasks are null
        return user?.Tasks?
            .OrderBy(task => task.Deadline ?? DateOnly.MaxValue) // Handle null deadlines
            .ToList() ?? new List<TaskProject>();
    }

    public async Task<DateOnly> SoonestAvailabilityForUser(string username)
    {
        // Get ordered tasks for the user
        var tasks = await GetTasksForUser(username);

        // Determine the latest deadline -> can be today if not other tasks or a deadline set in the future
        DateOnly latestDeadline = DateOnly.FromDateTime(DateTime.Now);

        foreach (var task in tasks)
        {
            if (task.Deadline.HasValue && task.Deadline.Value > latestDeadline)
            {
                latestDeadline = (DateOnly)task.Deadline;
            }
        }

        return latestDeadline;
    }


    public async Task<bool> IsOnHoliday(string username, DateOnly date)
    {
        IQueryable<Holiday> holiday =   context.Holidays.Include(u=> u.Owner).Where(h => h.Owner.Username==username).Where(h => h.TimeOffDate == date).AsQueryable(); 
        return (holiday != null);  //if no object, return false
      
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
    
    public async Task<IEnumerable<User>> GetUsersBySkillsAsync(SearchUserSkillFilterParametersDto parameters)
    {
        IQueryable<User> query = context.Users
            .Include(u => u.Department)
            .Include(u => u.UserSkills).ThenInclude(us => us.Skill)
            .AsQueryable();

        if (!string.IsNullOrEmpty(parameters.DepartmentName))
        {
            query = query.Where(u => u.Department.Name.ToLower().Equals(parameters.DepartmentName.ToLower()));
        }
        if (!string.IsNullOrEmpty(parameters.SkillName1))
        {
            if (parameters.Proficiency1.HasValue)
            {
                query = query.Where(u => u.UserSkills.Any(us => us.Skill.Name.ToLower().Contains(parameters.SkillName1.ToLower()) && us.Proficiency == parameters.Proficiency1));
            }
            else
            {
                query = query.Where(u => u.UserSkills.Any(us => us.Skill.Name.ToLower().Contains(parameters.SkillName1.ToLower())));
            }
        }
        if (!string.IsNullOrEmpty(parameters.SkillName2))
        {
            if (parameters.Proficiency2.HasValue)
            {
                query = query.Where(u => u.UserSkills.Any(us => us.Skill.Name.ToLower().Contains(parameters.SkillName2.ToLower()) && us.Proficiency == parameters.Proficiency2));
            }
            else
            {
                query = query.Where(u => u.UserSkills.Any(us => us.Skill.Name.ToLower().Contains(parameters.SkillName2.ToLower())));
            }
        }

        return await query.ToListAsync();
    }
}