using System.Text.Json.Serialization;

namespace Domain.Models;

public class TaskApproval
{
    public int Id { get; set; }
    public TaskProject TaskProject { get; set; }
    public int TaskProjectId { get; set; }
    public User Owner { get; set; }
    public string OwnerUsername { get; set; }  //manager
    public ApprovalStatus Status { get; set; }
    public string? Comments { get; set; }
    public DateOnly Date { get; set; }

    [JsonConstructor]
    public TaskApproval(int taskProjectId, string ownerUsername, ApprovalStatus status)
    {
        TaskProjectId = taskProjectId;
        OwnerUsername = ownerUsername;
        Status = status;
        Date = DateOnly.FromDateTime(DateTime.Now);
    }

    public TaskApproval()
    {
    }
}