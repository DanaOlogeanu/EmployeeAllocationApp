using System.Security.Claims;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    //login
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    
    //users on parameters
    Task<ICollection<User>> GetAsync(string? department, string?  skillOne, int? reqScoreOne, string? skillTwo, int? reqScoreTwo, string? skillThree, int? reqScoreThree); // to get list of users
}