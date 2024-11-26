using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class ProjectLogic : IProjectLogic
{
    private readonly IProjectDao projectDao;
    private readonly IUserDao userDao;

    public ProjectLogic(IProjectDao projectDao, IUserDao userDao)
    {
        this.projectDao = projectDao;
        this.userDao = userDao;
    }

    public async Task<Project> CreateAsync(ProjectCreationDto dto)
    {

        User? user = await userDao.GetByUsernameAsync(dto.OwnerUsername);

        if (user == null)
        {
            throw new Exception($"User with username {dto.OwnerUsername} was not found.");
        }

        ValidateData(dto);
        Project toCreate = new Project(user.Username, dto.ProjectName, dto.Description, dto.IsInvoicable, dto.StartDate,
            dto.Deadline, dto.ProjectStatus, dto.TagName, dto.ProjectPriority);
        Project created = await projectDao.CreateAsync(toCreate);

        return created;
    }
    

    private void ValidateData(ProjectCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.ProjectName)) throw new Exception("ProjectName cannot be empty.");
        if ((int)(dto.ProjectPriority) == 0) throw new Exception("Project priority cannot be empty.");
        if (string.IsNullOrEmpty(dto.TagName)) throw new Exception("Tag cannot be empty.");
    }
    
    public async Task<ProjectBasicDto?> GetByIdAsync(int id)
    {
        Project? project = await projectDao.GetByIdAsync(id);
        if (project == null)
        {
            throw new Exception($"Project with id {id} not found");
        }

        return new ProjectBasicDto (project.ProjectId, project.ProjectName, project.Description,project.OwnerUsername,project.IsInvoicable, project.StartDate,project.Deadline,project.ProjectStatus, project.TagName, project.ProjectPriority, project.Tasks);
    }

    
    public async Task UpdateAsync(ProjectBasicDto project)
    {
        Project? existing = await projectDao.GetByIdAsync(project.ProjectId);
        if (existing == null)
        {
            throw new Exception("Project not found");
        }

     
        existing.ProjectName = project.ProjectName;
        existing.Description = project.Description;
        existing.ProjectStatus = project.ProjectStatus;
        existing.StartDate = project.StartDate;
        existing.ProjectPriority = (Priority)project.ProjectPriority;
        existing.Deadline = project.Deadline;
        existing.TagName = project.TagName;
        existing.IsInvoicable = project.IsInvoicable;
     //  existing.OwnerUsername = project.OwnerUsername;//added
        //////////////////////added
        
        await projectDao.UpdateAsync(existing);
    }
    
    
    public Task<IEnumerable<Project>> GetProjects (string username)
    {
        return projectDao.GetProjects(username);
    }

    public Task<IEnumerable<Project>> SearchProjectsAsync(SearchProjectParameters parameters)
    {
        return projectDao.SearchProjectsAsync(parameters);
    }
    
}