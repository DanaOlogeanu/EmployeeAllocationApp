using System.Net.Http.Json;
using System.Text.Json;
using Domain.Dtos;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class TaskAssignmentLogHttpClient: ITaskAssignmentLogService
{
    
    private readonly HttpClient client; 
    public TaskAssignmentLogHttpClient (HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }
    
    public async Task<TaskAssignmentLog> CreateAsync (TaskAssignmentLogCreationDto dto)
    {
        
        HttpResponseMessage response = await client.PostAsJsonAsync("/TaskAssignmentLogs",dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        TaskAssignmentLog log = await response.Content.ReadFromJsonAsync<TaskAssignmentLog>();
        return log;  

    }

    public async Task<List<TaskAssignmentLogBasicDto>> GetTaskAssignmentLogsForProject(int taskProjectId)
    {
        HttpResponseMessage response = await client.GetAsync($"/TaskAssignmentLogs/taskProject/{taskProjectId}"); 
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            // Log the error response for troubleshooting
            Console.WriteLine($"Error: {result}");
            throw new Exception($"Error fetching task assigment logs for project: {response.ReasonPhrase}");
        }
       
        Console.WriteLine(result);
        List<TaskAssignmentLogBasicDto> projects = JsonSerializer.Deserialize<List<TaskAssignmentLogBasicDto>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return projects;
    }
}