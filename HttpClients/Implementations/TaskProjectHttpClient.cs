using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.Dtos;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class TaskProjectHttpClient:ITaskProjectService
{
    private readonly HttpClient client; 
    
    public TaskProjectHttpClient (HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }
    
    
    public async Task<TaskProject> CreateAsync(TaskProjectCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/TasksProject",dto);

        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        TaskProject project = await response.Content.ReadFromJsonAsync<TaskProject>();
        return project;  

    }

    public async Task<TaskProjectBasicDto?> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/TasksProject/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        TaskProjectBasicDto taskProject = JsonSerializer.Deserialize<TaskProjectBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return taskProject;
    }

    public async Task UpdateAsync(TaskProjectBasicDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/TasksProject", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<IEnumerable<TaskProject>> GetTasksUser(string username)
        {
            string uri = "/TasksProject/ViewTasks";
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
            IEnumerable<TaskProject> tasksProject = JsonSerializer.Deserialize<IEnumerable<TaskProject>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return tasksProject;
        }
    
}