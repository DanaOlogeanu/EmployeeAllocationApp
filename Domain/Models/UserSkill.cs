using System.Text.Json.Serialization;

namespace Domain.Models;

public class UserSkill
{
 

    public int UserSkillId { get; set; }
    public User Owner { get; set; }
    public string OwnerUsername { get; set; }
    public Skill Skill{ get; set; }
    public string SkillName { get; set; }
    public Proficiency Proficiency { get; set; }
    public string? Notes { get; set; }

    public UserSkill(User owner, Skill skill, Proficiency proficiency, string? notes)  
    {
        Owner = owner;
        Skill = skill;
        Proficiency = proficiency;
        Notes = notes;
        OwnerUsername = owner.Username;
    }

    [JsonConstructor]
    public UserSkill(string ownerUsername, string skillName, Proficiency proficiency, string? notes)
    {
        OwnerUsername = ownerUsername;
        SkillName = skillName;
        Proficiency = proficiency;
        Notes = notes;
    }
    
    
//no-arguments constructor for EFC to call when creating objects
private UserSkill() { }
}