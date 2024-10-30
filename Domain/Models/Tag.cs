namespace Domain.Models;

public class Tag
{
    public string Name { get; set; }
    public string Category { get; set; }

    public Tag(string name, string category)
    {
        Name = name;
        Category = category;
    }
}