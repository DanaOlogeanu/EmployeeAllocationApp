using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.Dtos;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class ProjectHttpClient: IProjectService
{
    
    private readonly HttpClient client; 
    public ProjectHttpClient (HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }
    
    
    public async Task<Project> CreateAsync(ProjectCreationDto dto)
    {
        
            HttpResponseMessage response = await client.PostAsJsonAsync("/Projects",dto);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }

            Project project = await response.Content.ReadFromJsonAsync<Project>();
            return project;  

        }

    public async Task<ProjectBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/Projects/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ProjectBasicDto project = JsonSerializer.Deserialize<ProjectBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return project;
    }

    public async Task UpdateAsync(ProjectBasicDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/Projects", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<IEnumerable<Project>> GetProjects(string username)
    {
        string uri = "/Projects/ViewProjects";
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
            throw new Exception($"Error fetching user projects: {response.ReasonPhrase}");
        }
       
        Console.WriteLine(result);
        IEnumerable<Project> projects = JsonSerializer.Deserialize<IEnumerable<Project>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return projects;
    }
    
}