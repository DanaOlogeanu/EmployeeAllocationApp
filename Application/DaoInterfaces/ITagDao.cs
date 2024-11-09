namespace Application.DaoInterfaces;

public interface ITagDao
{
    Task<List<string>> GetUniqueCategoriesAsync();
    Task<List<string>> GetTagsAsync(string cat);
}