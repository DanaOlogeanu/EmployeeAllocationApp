using System.Text.Json.Serialization;

namespace Domain.Models;

public class TaskAssignmentLog
{
    public int Id { get; set; }
    
    public TaskProject? TaskProject { get; set; }
    public int TaskProjectId { get; set; }
    public string AssignedBy { get; set; }
    public string AssignedTo { get; set; }
    public DateOnly Date { get; set; }

    [JsonConstructor]
    public TaskAssignmentLog(int taskProjectId, string assignedBy, string assignedTo)
    {
        TaskProjectId = taskProjectId;
        AssignedBy = assignedBy;
        AssignedTo = assignedTo;
        Date = DateOnly.FromDateTime(DateTime.Now);
    }

    public TaskAssignmentLog()
    {
        
    }
}