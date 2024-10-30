namespace Domain.Models;

public enum TaskStatus
{
    Created= 1,
    Requested = 2, //automatic once assigned
    Actioned,  //approved or not approved
    Approved,  //automatic once approved
    Assigned, //automatic view for employee
    Ongoing = 4,
    OnHold = 5,
    Completed = 6
}   