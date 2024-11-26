using Domain.Models;

namespace Domain.Dtos;

public class TaskProjectBasicDto
{
    public int Id { get; set; }
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
    
    // ICollection<TaskSkill>? TaskSkills { get; set; }

    // New property to track edit mode
   // public bool IsEditing { get; set; } = false;

    public TaskProjectBasicDto(int id, string name, int projectId, string? ownerUsername, int? estimate, TaskStatusEnum taskStatusEnum, DateOnly? startDate, DateOnly? deadline, int? dependentOn, double? orderNo, string? notes)
    {
        Id = id;
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

    // public static TaskProjectBasicDto fromModel(TaskProject task)
    // {
    //     return new TaskProjectBasicDto(task.Id, task.Name, task.ProjectId, task.OwnerUsername, task.Estimate,
    //         task.TaskStatusEnum, task.StartDate, task.Deadline, task.DependentOn, task.OrderNo, task.Notes)
    //     {
    //         TaskSkills = task.TaskSkills
    //     };
    // }
}