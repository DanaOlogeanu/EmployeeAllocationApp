using Domain.Models;

namespace Domain.Dtos
{
    public class SearchTaskSkillParametersDto
    {
        public int? TaskProjectId { get; set; }
        public string? SkillName { get; set; }
        public Proficiency? Proficiency { get; set; }

        public SearchTaskSkillParametersDto(int? taskProjectId, string? skillName, Proficiency? proficiency)
        {
            TaskProjectId = taskProjectId;
            SkillName = skillName;
            Proficiency = proficiency;
        }
    }
}