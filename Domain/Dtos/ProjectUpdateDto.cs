// using Domain.Models;
//
// namespace Domain.Dtos;
//
// public class ProjectUpdateDto
// {
//     public int ProjectId {get; set;}
//     public string? ProjectName { get; set; }
//     public string? Description { get; set; }
//     public bool IsInvoicable { get; set; }  // true = invoicable; false = not invoiceable
//     public DateOnly? StartDate { get; set; }
//     public DateOnly? Deadline { get; set; }
//     public ProjectStatus? ProjectStatus { get; set; }
//     public string? TagName { get; set; }
//     public Priority? ProjectPriority { get; set; }
//
//     public ProjectUpdateDto(int projectId, string? projectName, string? description, bool isInvoicable, DateOnly? startDate, DateOnly? deadline, ProjectStatus? projectStatus, string? tagName, Priority? projectPriority)
//     {
//         ProjectId = projectId;
//         ProjectName = projectName;
//         Description = description;
//         IsInvoicable = isInvoicable;
//         StartDate = startDate;
//         Deadline = deadline;
//         ProjectStatus = projectStatus;
//         TagName = tagName;
//         ProjectPriority = projectPriority;
//     }
// }