using System.Text.Json.Serialization;

namespace Domain.Models;

public class TaskSkill
{
    public int Id { get; set; }
    public TaskProject TaskProject { get; set; }
    public int TaskProjectId { get; set; }
    public Skill Skill { get; set; }
    public string SkillName { get; set; }
    public Proficiency Proficiency { get; set; }

  
    
    [JsonConstructor]
    public TaskSkill(int taskProjectId, string skillName, Proficiency proficiency)
    {
        TaskProjectId = taskProjectId;
        SkillName = skillName;
        Proficiency = proficiency;
    }
    
    public TaskSkill ()
    {}
}