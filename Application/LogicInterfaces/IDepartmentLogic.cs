namespace Application.LogicInterfaces;

public interface IDepartmentLogic
{
    Task<List<string>> GetUniqueDepartmentsAsync();
}