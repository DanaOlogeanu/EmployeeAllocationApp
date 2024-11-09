using System.Text.Json.Serialization;

namespace Domain.Models;

public class Holiday
{
    public int Id { get; set; }
    public User Owner { get; set; }
    public string OwnerUsername { get; set; }
    public DateOnly TimeOffDate { get; set; }

    [JsonConstructor]  
    public Holiday(string ownerUsername, DateOnly timeOffDate)
    {
        OwnerUsername = ownerUsername;
        TimeOffDate = timeOffDate;
    }

    //EFC
    public Holiday() { }
    
}