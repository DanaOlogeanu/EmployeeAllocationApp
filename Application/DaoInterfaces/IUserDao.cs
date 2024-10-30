using Domain.Dtos;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    //user search parameters
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    
    //log in
    Task<User?> GetByUsernameAsync(string username); 
    
    
    
}