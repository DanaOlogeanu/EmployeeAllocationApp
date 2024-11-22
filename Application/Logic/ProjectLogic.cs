using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class ProjectLogic : IProjectLogic
{
    private readonly IProjectDao projectDao;
    private readonly IUserDao userDao;
    private readonly ITaskSkillDao taskSkillDao;
    public ProjectLogic(IProjectDao projectDao, IUserDao userDao,ITaskSkillDao taskSkillDao)
    {
        this.projectDao = projectDao;
        this.userDao = userDao;
        this.taskSkillDao = taskSkillDao;
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

    
     public async Task<Project> DuplicateProject(ProjectBasicDto originalProject, string username)
    {
        Project? existing = await projectDao.GetByIdAsync(originalProject.ProjectId);
        if (existing == null)
        {
            throw new Exception("Project not found");
        }
        User? user = await userDao.GetByUsernameAsync(username);

        if (user == null)
        {
            throw new Exception($"User with username {user.Username} was not found.");
        }
        var newProject = new Project(
            ownerUsername: user.Username,
            projectName: originalProject.ProjectName + " (Copy)", // Modify name to avoid conflicts
            description: originalProject.Description,            // Copy description
            isInvoicable: originalProject.IsInvoicable,          // Copy invoicable status
            startDate: null,                                     // Reset start date
            deadline: null,                                      // Reset deadline
            projectStatus: ProjectStatus.Created,                                 // Reset status
            tagName: originalProject.TagName,                   // Copy tag
            projectPriority: originalProject.ProjectPriority                   // Reset priority
        )
        {
            Tasks = new List<TaskProject>() // Initialize the task list for the new project
        };

        // Duplicate tasks from the original project
        if (originalProject.Tasks != null)
        {
            foreach (var originalTask in originalProject.Tasks)
            {
               
                var newTask = new TaskProject
                {
                    Name = originalTask.Name + " (Copy)", // Modify name to avoid conflicts
                    Project = newProject,               // Link to the new project
                    Owner = null,                       // Reset task owner (assignee)
                    OwnerUsername = null,               // Reset owner username (assignee)
                    Estimate = originalTask.Estimate,   // Copy estimate
                    StartDate = null,                   // Reset start date
                    Deadline = null,                    // Reset deadline
                    DependentOn = originalTask.DependentOn, // Copy dependency
                    OrderNo = originalTask.OrderNo,     // Copy order number
                    SequenceNo = originalTask.SequenceNo,                  // Reset sequence number
                    Notes = originalTask.Notes,                       // Reset notes
                    TaskStatusEnum = TaskStatusEnum.Created, // Reset status to default
                    TaskSkills = new List<TaskSkill>()   // Copy skills
                };
                // Duplicate TaskSkills
                if (originalTask.TaskSkills != null)
                {
                    foreach (var originalSkill in originalTask.TaskSkills)
                    {
                        var newSkill = new TaskSkill(newTask.Id, originalSkill.SkillName, originalSkill.Proficiency)
                         {
                             TaskProject = newTask  // Link the duplicated skill to the new task
                        //     SkillName = originalSkill.SkillName,
                        //     Proficiency = originalSkill.Proficiency,
                        //  
                         };
                       newTask.TaskSkills.Add(newSkill); // Add the duplicated skill
                        // await taskSkillDao.CreateAsync(newSkill);
                    }
                }

                Console.WriteLine($"New project owner: {newProject.Owner?.Username}");
                newProject.Tasks.Add(newTask); // Add the duplicated task to the new project
                
                foreach (var task in newProject.Tasks)
                {
                    Console.WriteLine($"Task: {task.Name}, Project Owner: {task.Project.Owner?.Username}");
                }
            }
        }
        Project created = await projectDao.CreateAsync(newProject);
        
        return created;
    }
    
    
    // public async Task<Project> DuplicateProject(ProjectBasicDto originalProject, string username)
    // {
    //     Project? existing = await projectDao.GetByIdAsync(originalProject.ProjectId);
    //     if (existing == null)
    //     {
    //         throw new Exception("Project not found");
    //     }
    //     User? user = await userDao.GetByUsernameAsync(username);
    //
    //     if (user == null)
    //     {
    //         throw new Exception($"User with username {user.Username} was not found.");
    //     }
    //     var newProject = new Project(
    //         ownerUsername: user.Username,
    //         projectName: originalProject.ProjectName + " (Copy)", // Modify name to avoid conflicts
    //         description: originalProject.Description,            // Copy description
    //         isInvoicable: originalProject.IsInvoicable,          // Copy invoicable status
    //         startDate: null,                                     // Reset start date
    //         deadline: null,                                      // Reset deadline
    //         projectStatus: ProjectStatus.Created,                                 // Reset status
    //         tagName: originalProject.TagName,                   // Copy tag
    //         projectPriority: originalProject.ProjectPriority                   // Reset priority
    //     )
    //     {
    //         Tasks = new List<TaskProject>() // Initialize the task list for the new project
    //     };
    //
    //     // Duplicate tasks from the original project
    //     if (originalProject.Tasks != null)
    //     {
    //         foreach (var originalTask in originalProject.Tasks)
    //         {
    //            
    //             var newTask = new TaskProject
    //             {
    //                 Name = originalTask.Name + " (Copy)", // Modify name to avoid conflicts
    //                 Project = newProject,               // Link to the new project
    //                 Owner = null,                       // Reset task owner (assignee)
    //                 OwnerUsername = null,               // Reset owner username (assignee)
    //                 Estimate = originalTask.Estimate,   // Copy estimate
    //                 StartDate = null,                   // Reset start date
    //                 Deadline = null,                    // Reset deadline
    //                 DependentOn = originalTask.DependentOn, // Copy dependency
    //                 OrderNo = originalTask.OrderNo,     // Copy order number
    //                 SequenceNo = originalTask.SequenceNo,                  // Reset sequence number
    //                 Notes = originalTask.Notes,                       // Reset notes
    //                 TaskStatusEnum = TaskStatusEnum.Created, // Reset status to default
    //                 TaskSkills = originalTask.TaskSkills?.ToList() // Copy skills
    //             };
    //            
    //             Console.WriteLine($"New project owner: {newProject.Owner?.Username}");
    //             newProject.Tasks.Add(newTask); // Add the duplicated task to the new project
    //             
    //             foreach (var task in newProject.Tasks)
    //             {
    //                 Console.WriteLine($"Task: {task.Name}, Project Owner: {task.Project.Owner?.Username}");
    //             }
    //         }
    //     }
    //     Project created = await projectDao.CreateAsync(newProject);
    //     
    //     return created;
    // }

    
    public async Task<List<ProjectBasicDto>> GetProjectsByTagAsync(string tag)
    {
        List<ProjectBasicDto> result= new List<ProjectBasicDto>();
        List<Project> projects = await projectDao.GetProjectsByTagAsync(tag);
        foreach (Project project in projects)
        {
            var p = new ProjectBasicDto (project.ProjectId, project.ProjectName, project.Description,project.OwnerUsername,project.IsInvoicable, project.StartDate,project.Deadline,project.ProjectStatus, project.TagName, project.ProjectPriority, project.Tasks);
            result.Add(p);
        }

        return result;
    }
}

