using Domain.Models;

namespace Domain.Dtos;

public class UserSkillCreationDto
{
    public string Username { get; set; }
    public string SkillName { get; set; }
    public Proficiency Proficiency { get; set; }
    public string? Notes { get; set; }

    public UserSkillCreationDto(string username, string skillName, Proficiency proficiency, string? notes)
    {
        Username = username;
        SkillName = skillName;
        Proficiency = proficiency;
        Notes = notes;
    }
}