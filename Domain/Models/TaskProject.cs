using System.Text.Json.Serialization;

namespace Domain.Models;

public class TaskProject
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public User Owner { get; set; }
    public string OwnerUsername { get; set; }
    public int Estimate { get; set; } //hours?
    public TaskStatus TaskStatus { get; set;}
    public DateOnly? StartDate { get; set; }
    public DateOnly? Deadline { get; set; }
    public int DependentOn { get; set; } //if dependecy on another task - referenced by seq.no
    public double OrderNo { get; set; } //order of tasks for display
    public int SequenceNo { get; set;} // business key, generated - unique in combination with ProjectId, to avoid exposing tech key.
    public string Notes { get; set; }

    [JsonIgnore] //We have added two-way navigation properties to the domain classes, i.e. To do associates User, and User associates To do.
    //The Web API will return JSON. We cannot serialize objects to JSON if there are circular dependencies, which is what we have.
    public ICollection<TaskSkill> TaskSkills { get; set; }
    
    [JsonConstructor]
    public TaskProject(int projectId, string ownerUsername, int estimate, TaskStatus taskStatus, DateOnly? startDate, DateOnly? deadline, int dependentOn, double orderNo, int sequenceNo, string notes)
    {
        ProjectId = projectId;
        OwnerUsername = ownerUsername;
        Estimate = estimate;
        TaskStatus = taskStatus;
        StartDate = startDate;
        Deadline = deadline;
        DependentOn = dependentOn;
        OrderNo = orderNo;
        SequenceNo = sequenceNo;
        Notes = notes;
        TaskSkills = new List<TaskSkill>();
    }

    //efc
    public TaskProject()
    {
    }
}