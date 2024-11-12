using Domain.Models;

namespace Domain.Dtos
{
    public class TaskSkillCreationDto
    {
        public int TaskProjectId { get; set; }
        public string SkillName { get; set; }
        public Proficiency Proficiency { get; set; }

        public TaskSkillCreationDto(int taskProjectId, string skillName, Proficiency proficiency)
        {
            TaskProjectId = taskProjectId;
            SkillName = skillName;
            Proficiency = proficiency;
        }
    }
}