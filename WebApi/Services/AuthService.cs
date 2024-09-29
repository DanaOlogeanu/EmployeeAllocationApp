using Domain.Models;

namespace WebApi.Services;

public class AuthService: IAuthService
{
    /// <summary>
    /// REMOVE
    /// </summary>
    private readonly IList<User> users = new List<User>   //ADD DATABASE - ONLY FOR AUTHORIZATION/AUTHENTICATION TESTING
    {
        new User
        {
            Password = "password",
            Role = "Manager",
            Username = "ana",

        }
    };
    //
        
    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u =>     //ADD DATABASE INSTEAD OF USERS
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

}