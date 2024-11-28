using Domain.Models;

namespace Domain.Dtos
{
    public class SearchUserSkillFilterParametersDto
    {
        public string? SkillName1 { get; set; }
        public Proficiency? Proficiency1 { get; set; }
        public string? SkillName2 { get; set; }
        public Proficiency? Proficiency2 { get; set; }
        public string? DepartmentName { get; set; }
    }
}