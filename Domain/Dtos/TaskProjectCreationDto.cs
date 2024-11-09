using Domain.Models;

namespace Domain.Dtos;

public class TaskProjectCreationDto
{
    public string Name { get; set; }
    public int ProjectId { get; set; }
    public string? OwnerUsername { get; set; }
    public int? Estimate { get; set; } //hours?
    public TaskStatusEnum TaskStatusEnum { get; set;}
    public DateOnly? StartDate { get; set; }
    public DateOnly? Deadline { get; set; }
    public int? DependentOn { get; set; } //if dependecy on another task - referenced by seq.no
    public double? OrderNo { get; set; } //order of tasks for display
    public string? Notes { get; set; }

    
    public TaskProjectCreationDto(string name, int projectId, string? ownerUsername, int? estimate, TaskStatusEnum taskStatusEnum, DateOnly? startDate, DateOnly? deadline, int? dependentOn, double? orderNo, string? notes)
    {
        Name = name;
        ProjectId = projectId;
        OwnerUsername = ownerUsername;
        Estimate = estimate;
        TaskStatusEnum = taskStatusEnum;
        StartDate = startDate;
        Deadline = deadline;
        DependentOn = dependentOn;
        OrderNo = orderNo;
        Notes = notes;
    }
    
    //TODO add task skills 
}