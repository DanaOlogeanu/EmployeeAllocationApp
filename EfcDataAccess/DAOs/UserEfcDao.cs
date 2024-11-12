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
        // User? user = await context.Users.FindAsync(id);   //takes type int
        // return user;

        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }

    public async Task<IEnumerable<User>?> GetByDepartmentAsync(string selectedDpt)
    {
        IQueryable<User> usersQuery = context.Users.Include(user => user.Department).AsQueryable();
        if (!string.IsNullOrEmpty(selectedDpt))
        {
            usersQuery = usersQuery.Where(u => u.Department.Name.ToLower().Equals(selectedDpt.ToLower()));
        }
       
        IEnumerable<User>? result = await usersQuery.ToListAsync();
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

        // Determine the latest deadline
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
    
    
}