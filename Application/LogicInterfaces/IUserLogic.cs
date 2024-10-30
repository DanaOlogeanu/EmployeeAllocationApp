using Domain.Dtos;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
   // The data needed is wrapped in the UserCreationDto
   
   //search user params
   public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters); 
   
   //log in 
   Task<User> ValidateUser(string name, string password); 
   
   
   
}