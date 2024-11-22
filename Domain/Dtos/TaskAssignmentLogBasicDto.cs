namespace Domain.Dtos;

public class TaskAssignmentLogBasicDto
{
    public int Id { get; set; }
    public int TaskProjectId { get; set; }
    public string AssignedBy { get; set; }
    public string AssignedTo { get; set; }
    public DateOnly Date { get; set; }

    public TaskAssignmentLogBasicDto(int id, int taskProjectId, string assignedBy, string assignedTo, DateOnly date)
    {
        Id = id;
        TaskProjectId = taskProjectId;
        AssignedBy = assignedBy;
        AssignedTo = assignedTo;
        Date = date;
    }
}