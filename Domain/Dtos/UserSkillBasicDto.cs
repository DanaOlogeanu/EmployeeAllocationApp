using Domain.Models;

namespace Domain.Dtos;

public class UserSkillBasicDto
{
    public int UserSkillId { get; set; }
    public string UserName { get; set; }
    public string SkillName{ get; set; }
    public Proficiency Proficiency { get; set; }
    public string Notes { get; set; }

    public UserSkillBasicDto(int userSkillId, string userName, string skillName, Proficiency proficiency, string notes)
    {
        UserSkillId = userSkillId;
        UserName = userName;
        SkillName = skillName;
        Proficiency = proficiency;
        Notes = notes;
    }
}