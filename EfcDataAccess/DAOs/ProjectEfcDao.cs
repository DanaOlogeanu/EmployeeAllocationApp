using Application.DaoInterfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class ProjectEfcDao:IProjectDao
{
    
    private readonly AppContext context;

    public ProjectEfcDao(AppContext context)
    {
        this.context = context;
    }
    
    
    public async Task<Project> CreateAsync(Project project)
    {
     
        EntityEntry<Project> added = await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();
        return added.Entity;
    }

   

    public async Task<Project?> GetByIdAsync(int id)
    {
        Project? found = await context.Projects
            //  .AsNoTracking()
          .Include(us => us.Tasks) 
            .SingleOrDefaultAsync(us=> us.ProjectId == id);
        return found;
    }

    public async Task UpdateAsync(Project project)
    {
        context.Projects.Update(project);
        await context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Project>> GetProjects(string username)
    {
        IQueryable<Project> projectsQuery = context.Projects.Include (us => us.Owner).AsQueryable();
        if (!string.IsNullOrEmpty(username))
        {
            projectsQuery = projectsQuery.Where(u => u.Owner.Username == username);
        }
   
        List<Project> result = await projectsQuery.ToListAsync();
        return result; 
    }

    public async Task<Project> DuplicateProject(Project originalProject, string username)
    {
        EntityEntry<Project> added = await context.Projects.AddAsync(originalProject);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<List<Project>> GetProjectsByTagAsync(string tag)
    {
        
        IQueryable<Project> projectsQuery = context.Projects.Include(p=>p.Tasks).ThenInclude( t=>t.TaskSkills).Include(p => p.Owner)
            .AsQueryable();
        if (!string.IsNullOrEmpty(tag))
        {
            projectsQuery = projectsQuery.Where(u => u.TagName == tag);
        }
   
        List<Project> result = await projectsQuery.ToListAsync();
        return result; 
    }
}