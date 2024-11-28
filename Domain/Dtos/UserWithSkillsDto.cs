using Domain.Models;

namespace Domain.Dtos
{
    public class UserWithSkillsDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public List<UserSkillBasicDto> UserSkills { get; set; } = new();

    
        public UserWithSkillsDto() { }
        
        public UserWithSkillsDto(string username, string name, string departmentName, List<UserSkillBasicDto> userSkills)
        {
            Username = username;
            Name = name;
            DepartmentName = departmentName;
            UserSkills = userSkills;
        }
    }
}