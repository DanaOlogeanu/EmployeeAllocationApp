using Domain.Models;

namespace Domain.Dtos;

public class TaskApprovalCreationDto
{
    
    public int TaskProjectId { get; set; }
    public string OwnerUsername { get; set; }
    public ApprovalStatus Status { get; set; }
   

    public TaskApprovalCreationDto(int taskProjectId, string ownerUsername, ApprovalStatus status)
    {
        TaskProjectId = taskProjectId;
        OwnerUsername = ownerUsername;
        Status = status;
      
    
    }
}