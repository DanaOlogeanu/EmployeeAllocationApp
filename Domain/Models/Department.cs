namespace Domain.Models;

public class Department
{
    public string Name { get; set; }
    public ICollection<User> Users { get; set; }
    
    
}