namespace Domain.Models;

public class Skill
{
    public string Name { get; set; }
    public string Category{ get; set; }

    public Skill(string name,string category)
    {
        Name = name;
        Category = category;
    }
}