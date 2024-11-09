namespace Domain.Models;

public enum TaskStatusEnum
{
    Created= 1,
    Requested = 2, //automatic once assigned
    Actioned =3,  //approved or not approved
    Approved=4,  //automatic once approved
    Assigned=5, //automatic view for employee
    Ongoing = 6,
    OnHold = 7,
    Completed = 8
}   