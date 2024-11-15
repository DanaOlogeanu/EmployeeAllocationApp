using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.Dtos;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class TaskApprovalHttpClient:ITaskApprovalService
{
    private readonly HttpClient client; 
    
    public TaskApprovalHttpClient (HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }

    public async Task<TaskApproval> CreateAsync(TaskApprovalCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/TasksApprovals",dto);

        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        TaskApproval taskApproval = await response.Content.ReadFromJsonAsync<TaskApproval>();
        return taskApproval;  
    }

    public async Task<IEnumerable<TaskApproval>> GetApprovalsManager(string username)
    {
        string uri = "/TasksApprovals/ViewApprovals";
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
            throw new Exception($"Error fetching user approvals: {response.ReasonPhrase}");
        }
       
        Console.WriteLine(result);
        IEnumerable<TaskApproval> tasksApprovals = JsonSerializer.Deserialize<IEnumerable<TaskApproval>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return tasksApprovals;
    }

    public async Task UpdateAsync(TaskApprovalBasicDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/TasksApprovals", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<TaskApprovalBasicDto?> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/TasksApprovals/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        TaskApprovalBasicDto taskApproval = JsonSerializer.Deserialize<TaskApprovalBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return taskApproval;
    }
}