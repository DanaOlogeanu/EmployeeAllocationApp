using System.Text.Json.Serialization;

namespace Domain.Models;

public class Availability
{
    public int Id { get; set; }
    public User Owner { get; set; }
    public string OwnerUsername { get; set; }
    public int WeekOfYear { get; set; }
    public int ProjectHoursPerWeek { get; set; }
    public int TimeOffHours { get; set;}
    public int AvailableHoursPerWeek { get; set; }
    
    [JsonConstructor]  
    public Availability(string ownerUsername, int weekOfYear, int projectHoursPerWeek, int timeOffHours)
    {
        OwnerUsername = ownerUsername;
        WeekOfYear = weekOfYear;
        ProjectHoursPerWeek = projectHoursPerWeek;
        TimeOffHours = timeOffHours;
        AvailableHoursPerWeek = projectHoursPerWeek - timeOffHours;  //????
    }

    //EFC
    public Availability() { }
    
}