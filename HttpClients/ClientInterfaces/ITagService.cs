namespace HttpClients.ClientInterfaces;

public interface ITagService
{
    Task<List<string>> GetUniqueCategories();
    Task<List<string>> GetTags(string? cat);
}