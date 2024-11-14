using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Name { get; set; }
    public string? Role { get; set; }
    public Department? Department { get; set; }
    public string? Email { get; set; }
    public int? ProjectHoursPerDay { get; set; }

    [JsonIgnore] //We have added two-way navigation properties to the domain classes, i.e. To do associates User, and User associates To do.
    //The Web API will return JSON. We cannot serialize objects to JSON if there are circular dependencies, which is what we have.
    public ICollection<UserSkill>? UserSkills { get; set; } =new List<UserSkill>();
    [JsonIgnore]
    public ICollection<Holiday>? Holidays { get; set; } =new List<Holiday>();
    [JsonIgnore]
    public ICollection<TaskProject>? Tasks { get; set; } =new List<TaskProject>();
    //add collection projects??? for proj manager???
    
   // [JsonConstructor]
    public User(string username, string password, string? name,string? role, Department? department, string? email, int? projectHoursPerDay, ICollection<UserSkill>? userSkills,  ICollection<Holiday>? holidays , ICollection<TaskProject>? tasks) 
    {
        Username = username;
        Password = password;
        Name = name;
        Role = role;
        Department = department;
        Email = email;
        ProjectHoursPerDay = projectHoursPerDay;
        UserSkills = userSkills;
        Holidays = holidays;
        Tasks = tasks;
    }
    private User(){}

}