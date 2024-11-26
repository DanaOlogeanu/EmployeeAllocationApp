namespace Domain.Dtos
{
    public class UserMatrixDto
    {
        public string Username { get; set; }
        public string? Name { get; set; }
        public List<UserSkillMatrixDto> Skills { get; set; }

        public UserMatrixDto(string username, string? name)
        {
            Username = username;
            Name = name;
            Skills = new List<UserSkillMatrixDto>();
        }

        public UserMatrixDto() 
        {
            Skills = new List<UserSkillMatrixDto>();
        }
    }
}