using Domain.Models;

namespace Domain.Dtos
{
    public class UserSkillMatrixDto
    {
        public string SkillName { get; set; }
        public Proficiency Proficiency { get; set; }

        public UserSkillMatrixDto(string skillName, Proficiency proficiency)
        {
            SkillName = skillName;
            Proficiency = proficiency;
        }

        public UserSkillMatrixDto() { }
    }
}