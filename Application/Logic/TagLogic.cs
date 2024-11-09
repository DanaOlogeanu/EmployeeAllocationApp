using Application.DaoInterfaces;
using Application.LogicInterfaces;

namespace Application.Logic;

public class TagLogic: ITagLogic
{
    private readonly ITagDao tagDao;
  

    public TagLogic(ITagDao tagDao)
    {
        this.tagDao = tagDao;
    }

    public async Task<List<string>> GetUniqueCategoriesAsync()
    {
        List<string> categories = await tagDao.GetUniqueCategoriesAsync();
        if (categories == null)
        {
            throw new Exception($"Tag categories not found");
        }

        return categories;

    }

    public async Task<List<string>> GetTagsAsync(string cat)
    {
        List<string> skills = await tagDao.GetTagsAsync(cat);
        if (skills == null)
        {
            throw new Exception($"Tags by category not found");
        }

        return skills;
    }
}