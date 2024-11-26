namespace Domain.Dtos;

public class TaskAssignmentLogCreationDto
{
     

    public int TaskProjectId { get; set; }
    public string AssignedBy { get; set; }
    public string AssignedTo { get; set; }
    
    
    public TaskAssignmentLogCreationDto(int taskProjectId, string assignedBy, string assignedTo)
    {
        TaskProjectId = taskProjectId;
        AssignedBy = assignedBy;
        AssignedTo = assignedTo;
       
    }
}