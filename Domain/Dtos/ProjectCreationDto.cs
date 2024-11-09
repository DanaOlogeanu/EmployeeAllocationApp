using Domain.Models;

namespace Domain.Dtos;

public class ProjectCreationDto
{
    public string OwnerUsername { get; set; }
    public string ProjectName { get; set; }
    public string? Description { get; set; }
    public bool IsInvoicable { get; set; }  // true = invoicable; false = not invoiceable
    public DateOnly? StartDate { get; set; }
    public DateOnly? Deadline { get; set; }
    public ProjectStatus? ProjectStatus { get; set; }
    public string TagName { get; set; }
    public Priority ProjectPriority { get; set; }
    public ICollection<TaskProject>? Tasks { get; set; }
    
    
    public ProjectCreationDto(string ownerUsername,string projectName, string? description, bool isInvoicable, DateOnly? startDate, DateOnly? deadline, ProjectStatus? projectStatus, string tagName, Priority projectPriority)
    {
        OwnerUsername = ownerUsername;
        ProjectName = projectName;
        Description = description;
        IsInvoicable = isInvoicable;
        StartDate = startDate;
        Deadline = deadline;
        ProjectStatus = projectStatus;
        TagName = tagName;
        ProjectPriority = projectPriority;
    }
}