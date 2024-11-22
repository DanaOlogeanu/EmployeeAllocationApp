using System.Text.Json.Serialization;
using Domain.Models;

namespace Domain.Dtos;

public class ProjectBasicDto
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string? Description { get; set; }
    public string OwnerUsername { get; set; }
    public bool? IsInvoicable { get; set; }  // true = invoicable; false = not invoiceable
    public DateOnly? StartDate { get; set; }
    public DateOnly? Deadline { get; set; }
    public ProjectStatus? ProjectStatus { get; set; }
    public string TagName { get; set; }
    public Priority ProjectPriority { get; set; }
    
    public ICollection<TaskProject>? Tasks { get; set; }

    public ProjectBasicDto(int projectId, string projectName, string? description, string ownerUsername, bool? isInvoicable, DateOnly? startDate, DateOnly? deadline, ProjectStatus? projectStatus, string tagName, Priority projectPriority, ICollection<TaskProject>? tasks)
    {
        ProjectId = projectId;
        ProjectName = projectName;
        Description = description;
        OwnerUsername = ownerUsername;
        IsInvoicable = isInvoicable;
        StartDate = startDate;
        Deadline = deadline;
        ProjectStatus = projectStatus;
        TagName = tagName;
        ProjectPriority = projectPriority;
        Tasks = tasks;
    }
    
}