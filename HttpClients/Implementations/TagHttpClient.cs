using System.Text.Json;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class TagHttpClient :ITagService
    {
    
    private readonly HttpClient client; 
    
    public TagHttpClient (HttpClient client) //We request an HttpClient through the constructor, thereby leaving the creation of the HttpClient up to the Blazor framework
    {
        this.client = client;
    }

    public async Task<List<string>> GetUniqueCategories()
    {
        HttpResponseMessage response = await client.GetAsync("/Tags");
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
    

    public async Task<List<string>> GetTags(string? category)
    {
        string query = "";
        if ((!string.IsNullOrEmpty(category)))
        {
            query += $"?category={category}";
        }
        HttpResponseMessage response = await client.GetAsync($"/Tags/TagByCat"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        List<string> tags = JsonSerializer.Deserialize<List<string>>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;   // null-suppressor "!"
        return tags;
    }
    }