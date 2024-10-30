using Domain.Models;

namespace Domain.Dtos;

public class SearchUserSkillParametersDto
{
    public string? Username { get; }
    public string? SkillName { get; }
    public Proficiency? Proficiency { get; }

    public SearchUserSkillParametersDto(string? username, string? skillName, Proficiency? proficiency)
    {
        Username = username;
        SkillName = skillName;
        Proficiency = proficiency;
    }
}