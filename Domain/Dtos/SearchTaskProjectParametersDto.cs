using Domain.Models;

namespace Domain.Dtos
{
    public class SearchTaskProjectParametersDto
    {
        public int? Id { get; set; }
        public string? TaskName { get; set; }
        public string? OwnerUsername { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? Deadline { get; set; }
        public TaskStatusEnum? TaskStatus { get; set; }
        /*public Priority? TaskPriority { get; set; }*/

        public SearchTaskProjectParametersDto(int? id, string? taskName, string? ownerUsername, DateOnly? startDate, DateOnly? deadline, TaskStatusEnum? taskStatus, Priority? taskPriority)
        {
            Id = id;
            TaskName = taskName;
            OwnerUsername = ownerUsername;
            StartDate = startDate;
            Deadline = deadline;
            TaskStatus = taskStatus;
            /*TaskPriority = taskPriority;*/
        }

        public SearchTaskProjectParametersDto() { }
    }
}