using Domain.Models;

namespace Domain.Dtos
{
    public class TaskSkillBasicDto
    {
        public int TaskSkillId { get; set; }
        public int TaskProjectId { get; set; }
        public string SkillName { get; set; }
        public Proficiency Proficiency { get; set; }

        public TaskSkillBasicDto(int taskSkillId, int taskProjectId, string skillName, Proficiency proficiency)
        {
            TaskSkillId = taskSkillId;
            TaskProjectId = taskProjectId;
            SkillName = skillName;
            Proficiency = proficiency;
        }
    }
}