using System.Text.Json.Serialization;

namespace Domain.Models;

public class Department
{
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<User> Users { get; set; }
    
    
}