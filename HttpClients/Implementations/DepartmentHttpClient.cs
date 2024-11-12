using System.Text.Json;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class DepartmentHttpClient:IDepartmentService
{
    private readonly HttpClient client; 
    public DepartmentHttpClient (HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }
    
    
    public async Task<List<string>> GetUniqueDepartmentsAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/Departments");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        List<string> dptss = JsonSerializer.Deserialize<List<string>>(result, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return dptss;
    }
}