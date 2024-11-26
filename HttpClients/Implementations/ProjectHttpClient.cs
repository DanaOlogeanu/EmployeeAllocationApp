using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                PropertyNameCaseInsensitive = true,  ReferenceHandler = ReferenceHandler.Preserve 
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
            PropertyNameCaseInsensitive = true,  ReferenceHandler = ReferenceHandler.Preserve 
        })!;
        return projects;
    }

    public async Task<Project> DuplicateProject(ProjectBasicDto originalProject, string username)
    {
        if (originalProject == null)
        {
            throw new ArgumentNullException(nameof(originalProject), "Original project cannot be null.");
        }

        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(username));
        }

        // Construct the URI with the username query parameter
        string uri = $"/Projects/Duplicate?userName={Uri.EscapeDataString(username)}";

        // Send the POST request with the original project data
        HttpResponseMessage response = await client.PostAsJsonAsync(uri, originalProject);

        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error duplicating project: {content}");
        }

        // Deserialize and return the duplicated project
        Project project = await response.Content.ReadFromJsonAsync<Project>();
        return project;
    }
    
    // public async Task<Project> DuplicateProject(ProjectBasicDto originalProject,string username)
    // {
    //  
    //     
    //     HttpResponseMessage response = await client.PostAsJsonAsync("/Projects/Duplicate",originalProject);
    //     if (!response.IsSuccessStatusCode)
    //     {
    //         string content = await response.Content.ReadAsStringAsync();
    //         throw new Exception(content);
    //     }
    //
    //     Project project = await response.Content.ReadFromJsonAsync<Project>();
    //     return project;  
    // }

    public async Task<List<ProjectBasicDto>> GetProjectsByTagAsync(string tag)
    {
        string uri = "/Projects/ViewProjectsByTag";
        if (!string.IsNullOrEmpty(tag))
        {
            uri += $"?tag={tag}";
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
        List<ProjectBasicDto> projects = JsonSerializer.Deserialize<List<ProjectBasicDto>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,  ReferenceHandler = ReferenceHandler.Preserve 
        })!;
        return projects;
    }
    
    public async Task<IEnumerable<Project>> GetProjectsByParameters(SearchProjectParameters parameters)
    {
        string uri = "/Projects/SearchProjects";
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(parameters.ProjectName))
        {
            queryParams.Add($"projectName={parameters.ProjectName}");
        }
        if (!string.IsNullOrEmpty(parameters.OwnerUsername))
        {
            queryParams.Add($"ownerUsername={parameters.OwnerUsername}");
        }
        if (parameters.IsInvoicable.HasValue)
        {
            queryParams.Add($"isInvoicable={parameters.IsInvoicable}");
        }
        if (parameters.StartDate.HasValue)
        {
            queryParams.Add($"startDate={parameters.StartDate.Value.ToString("yyyy-MM-dd")}");
        }
        if (parameters.Deadline.HasValue)
        {
            queryParams.Add($"deadline={parameters.Deadline.Value.ToString("yyyy-MM-dd")}");
        }
        if (parameters.ProjectStatus.HasValue)
        {
            queryParams.Add($"projectStatus={parameters.ProjectStatus}");
        }
        if (!string.IsNullOrEmpty(parameters.TagName))
        {
            queryParams.Add($"tagName={parameters.TagName}");
        }
        if (parameters.ProjectPriority.HasValue)
        {
            queryParams.Add($"projectPriority={parameters.ProjectPriority}");
        }

        if (queryParams.Any())
        {
            uri += "?" + string.Join("&", queryParams);
        }

        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            // Log the error response for troubleshooting
            Console.WriteLine($"Error: {result}");
            throw new Exception($"Error fetching projects: {response.ReasonPhrase}");
        }

        IEnumerable<Project> projects = JsonSerializer.Deserialize<IEnumerable<Project>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return projects;
    }
}
