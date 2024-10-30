using System.Text.Json;
using Domain.Dtos;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class SkillHttpClient:ISkillService
{
    
    private readonly HttpClient client; 
    public SkillHttpClient (HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }
    
    public async Task<SkillBasicDto> GetByNameAsync(string name)
    {
        string uri = "/";
        if (!string.IsNullOrEmpty(name))
        {
            uri += $"?Name={name}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        SkillBasicDto skill = JsonSerializer.Deserialize<SkillBasicDto>(result, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return skill;
    }

    public async Task<List<string>> GetUniqueCategories()
    {
        HttpResponseMessage response = await client.GetAsync("/Skills");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        List<string> categories = JsonSerializer.Deserialize<List<string>>(result, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return categories;
    }

   
    
    public async Task<List<string>> GetUniqueSkills(string category)
    {
        string query = "";
        if ((!string.IsNullOrEmpty(category)))
        {
            query += $"?category={category}";
        }
        HttpResponseMessage response = await client.GetAsync($"/Skills/SkillByCat"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        List<string> skill = JsonSerializer.Deserialize<List<string>>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return skill;
    }
}