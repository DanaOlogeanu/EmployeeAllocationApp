namespace Domain.Dtos;

public class SkillBasicDto
{
    public string Name { get; set; }
    public string Category{ get; set; }

    public SkillBasicDto(string name,string category)
    {
        Name = name;
        Category = category;
    }
}