namespace Application.DaoInterfaces;

public interface IDepartmentDao
{
    Task<List<string>> GetUniqueDepartmentsAsync();
}