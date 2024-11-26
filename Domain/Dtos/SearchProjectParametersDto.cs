using Domain.Models;

namespace Domain.Dtos
{
    public class SearchProjectParameters
    {
        public string? ProjectName { get; set; }
        public string? OwnerUsername { get; set; }
        public bool? IsInvoicable { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? Deadline { get; set; }
        public ProjectStatus? ProjectStatus { get; set; }
        public string? TagName { get; set; }
        public Priority? ProjectPriority { get; set; }

        public SearchProjectParameters(string? projectName, string? ownerUsername, bool? isInvoicable, DateOnly? startDate, DateOnly? deadline, ProjectStatus? projectStatus, string? tagName, Priority? projectPriority)
        {
            ProjectName = projectName;
            OwnerUsername = ownerUsername;
            IsInvoicable = isInvoicable;
            StartDate = startDate;
            Deadline = deadline;
            ProjectStatus = projectStatus;
            TagName = tagName;
            ProjectPriority = projectPriority;
        }
        public SearchProjectParameters() { }
    }
}