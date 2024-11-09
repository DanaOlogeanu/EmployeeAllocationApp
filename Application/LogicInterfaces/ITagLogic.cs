namespace Application.LogicInterfaces;

public interface ITagLogic
{
    Task<List<string>> GetUniqueCategoriesAsync();
    Task<List<string>> GetTagsAsync(string cat);
}