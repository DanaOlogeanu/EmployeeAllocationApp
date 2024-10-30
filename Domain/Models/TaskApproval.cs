using System.Text.Json.Serialization;

namespace Domain.Models;

public class TaskApproval
{
    public int Id { get; set; }
    public TaskProject TaskProject { get; set; }
    public int TaskProjectId { get; set; }
    public User Owner { get; set; }
    public string OwnerUsername { get; set; }
    public ApprovalStatus Status { get; set; }
    public string Comments { get; set; }
    public DateOnly Date { get; set; }

    [JsonConstructor]
    public TaskApproval(int taskProjectId, string ownerUsername, ApprovalStatus status, string comments)
    {
        TaskProjectId = taskProjectId;
        OwnerUsername = ownerUsername;
        Status = status;
        Comments = comments;
    }

    public TaskApproval()
    {
    }
}