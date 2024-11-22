using System.Text.Json.Serialization;

namespace Domain.Models;

public class Project
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string? Description { get; set; }
    
    public User? Owner { get; set; }
    public string OwnerUsername { get; set; }
    public bool? IsInvoicable { get; set; }  // true = invoicable; false = not invoiceable
    public DateOnly? StartDate { get; set; }
    public DateOnly? Deadline { get; set; }
    public ProjectStatus? ProjectStatus { get; set; }
    public Tag? Tag { get; set; }
    public string TagName { get; set; }
    public Priority ProjectPriority { get; set; }

     [JsonIgnore] //We have added two-way navigation properties to the domain classes, i.e. To do associates User, and User associates To do.
    //The Web API will return JSON. We cannot serialize objects to JSON if there are circular dependencies, which is what we have.
    public ICollection<TaskProject>? Tasks { get; set; }

    [JsonConstructor]  
    public Project(string ownerUsername, string projectName, string? description, bool? isInvoicable, DateOnly? startDate, DateOnly? deadline, ProjectStatus? projectStatus, string tagName, Priority projectPriority)
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
        Tasks = new List<TaskProject>();
    }


    //efc
    public Project()
    {
    }
}