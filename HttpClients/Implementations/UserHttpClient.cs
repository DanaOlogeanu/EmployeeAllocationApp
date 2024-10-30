using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Domain.Dtos;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserHttpClient: IUserService
{
    private readonly HttpClient client; 
    public static string? Jwt { get; private set; } = "";
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    public UserHttpClient(HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }
    
    //list of users parameters
    public async Task<ICollection<User>> GetAsync(string? department, string?  skillOne, int? reqScoreOne, string? skillTwo, int? reqScoreTwo, string? skillThree, int? reqScoreThree)
    {
        string query = ConstructQuery(department, skillOne, reqScoreOne, skillTwo, reqScoreTwo, skillThree, reqScoreThree);
        HttpResponseMessage response = await client.GetAsync("/users" + query);

        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<User> users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return users;
    }

    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!rethink
    private static string ConstructQuery(string? department, string?  skillOne, int? reqScoreOne, string? skillTwo, int? reqScoreTwo, string? skillThree, int? reqScoreThree)
    {
        string query = "";
        if (!string.IsNullOrEmpty(department))
        {
            query += $"?department={department}";
        }
        
        if (!string.IsNullOrEmpty(skillOne))
        {
            query += $"?d={skillOne}";
        }
        if (reqScoreOne!= null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"reqScoreOne={reqScoreOne}";
        }
        if (!string.IsNullOrEmpty(skillTwo))
        {
            query += $"?d={skillTwo}";
        }
        if (reqScoreTwo != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"reqScoreTwo={reqScoreTwo}";
        }
        if (!string.IsNullOrEmpty(skillThree))
        {
            query += $"?d={skillThree}";
        }
        if (reqScoreThree != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"reqScoreThree={reqScoreThree}";
        }

        return query;
    }

    
    //LOG IN
     // this private variable for simple caching
   

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
    
    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
    
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
    
    public async Task LoginAsync(string username, string password)
    {
        UserLoginDto userLoginDto = new()
        {
            Username = username,
            Password = password
        };

        string userAsJson = JsonSerializer.Serialize(userLoginDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("/auth/login", content);   //LOCAL HOST ID
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        string token = responseContent;
        Jwt = token;

        ClaimsPrincipal principal = CreateClaimsPrincipal();

        OnAuthStateChanged.Invoke(principal);
    }
    
    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }
    
    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }

    

    
}