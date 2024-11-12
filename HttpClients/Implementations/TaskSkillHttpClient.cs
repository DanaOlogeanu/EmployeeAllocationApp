using System.Net.Http.Json;
using System.Text.Json;
using Domain.Dtos;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations
{
    public class TaskSkillHttpClient : ITaskSkillService
    {
        private readonly HttpClient client;
        public TaskSkillHttpClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<TaskSkill> CreateAsync(TaskSkillCreationDto dto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/TaskSkills", dto);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }

            TaskSkill taskSkill = await response.Content.ReadFromJsonAsync<TaskSkill>();
            return taskSkill;
        }

        public async Task<ICollection<TaskSkill>> GetAsync(string? skillName, int? proficiency, int? taskProjectId)
        {
            string query = ConstructQuery(skillName, proficiency, taskProjectId);
            HttpResponseMessage response = await client.GetAsync("/TaskSkills" + query);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            ICollection<TaskSkill> skills = JsonSerializer.Deserialize<ICollection<TaskSkill>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return skills;
        }

        private static string ConstructQuery(string? skillName, int? proficiency, int? taskProjectId)
        {
            string query = "";
            if (taskProjectId != null)
            {
                query += $"?taskProjectId={taskProjectId}";
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

        public async Task<TaskSkillBasicDto> GetByIdAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"/TaskSkills/{id}");
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            TaskSkillBasicDto skill = JsonSerializer.Deserialize<TaskSkillBasicDto>(content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return skill;
        }

        public async Task UpdateAsync(TaskSkillBasicDto taskSkill)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"/TaskSkills/{taskSkill.TaskSkillId}", taskSkill);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }
        }

        public async Task DeleteAsync(int taskSkillId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"/TaskSkills/{taskSkillId}");
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }
        }
    }
}

