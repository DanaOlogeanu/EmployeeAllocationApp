using Domain.Models;

namespace Domain.Dtos;

public class TaskApprovalBasicDto
{
    public int TaskApprovalId { get; set; }
    public int? TaskProjectId { get; set; }
    public string? OwnerUsername { get; set; } //manager
    public ApprovalStatus? Status { get; set; }
    public string? Comments { get; set; }
    public DateOnly? Date { get; set; } //set to now

    public TaskApprovalBasicDto(int taskApprovalId, int? taskProjectId, string? ownerUsername, ApprovalStatus? status, string? comments, DateOnly? date)
    {
        TaskApprovalId = taskApprovalId;
        TaskProjectId = taskProjectId;
        OwnerUsername = ownerUsername;
        Status = status;
        Comments = comments;
        Date = date;
    }
}