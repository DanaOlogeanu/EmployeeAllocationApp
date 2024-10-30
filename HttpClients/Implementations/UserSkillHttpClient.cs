using System.Net.Http.Json;
using System.Text.Json;
using Domain.Dtos;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserSkillHttpClient: IUserSkillService
{
    private readonly HttpClient client; 
    public UserSkillHttpClient (HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }

    //create userskill -make a POST request with the JSON. 
    public async Task<UserSkill> CreateAsync(UserSkillCreationDto dto)
    {
        
        HttpResponseMessage response = await client.PostAsJsonAsync("/UserSkills",dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        UserSkill userSkill = await response.Content.ReadFromJsonAsync<UserSkill>();
        return userSkill;  

    }
    
    //get list of skills accordding to username, skillname or proficiency  //general   --> Hide UserName field for employee, on load own skills 
    public async Task<ICollection<UserSkill>> GetAsync(string? userName, string? skillName, int? proficiency)
    {
       // string url = "http://localhost:5127";
        string query = ConstructQuery(userName,skillName,proficiency);
        HttpResponseMessage response = await client.GetAsync("/UserSkills"+ query); //controller route
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<UserSkill> skills = JsonSerializer.Deserialize<ICollection<UserSkill>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return skills;
    }

    private static string ConstructQuery(string? userName, string? skillName, int? proficiency)
    {
        string query = "";
        if (userName != null)
        {
            query += $"?userName={userName}";
        }

        if (skillName != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"skillName={skillName}";
        }

        if (proficiency != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"proficiency={proficiency}";
        }
        return query;
    }
    
    //get userskill by id 
    public async Task<UserSkillBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/UserSkills/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        UserSkillBasicDto skill = JsonSerializer.Deserialize<UserSkillBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return skill;
    }
    
    
    
    public async Task<IEnumerable<UserSkill>> GetUserSkills(string username)
    {
        // HttpResponseMessage response = await client.GetAsync($"/viewUserSkills");
        // string content = await response.Content.ReadAsStringAsync();
        // if (!response.IsSuccessStatusCode)
        // {
        //     throw new Exception(content);
        // }
        //
        // IEnumerable<UserSkill> userSkills = JsonSerializer.Deserialize<IEnumerable<UserSkill>>(content, new JsonSerializerOptions
        //     {
        //         PropertyNameCaseInsensitive = true
        //     }
        // )!;   // null-suppressor "!"
        // return userSkills;
        
         string uri = "/UserSkills/ViewUserSkills";
        if (!string.IsNullOrEmpty(username))
        {
            uri += $"?userName={username}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            // Log the error response for troubleshooting
            Console.WriteLine($"Error: {result}");
            throw new Exception($"Error fetching user skills: {response.ReasonPhrase}");
        }
       
        Console.WriteLine(result);
        IEnumerable<UserSkill> userSkills = JsonSerializer.Deserialize<IEnumerable<UserSkill>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return userSkills;
    }
}