namespace HttpClients.ClientInterfaces;

public interface IDepartmentService
{
    Task<List<string>> GetUniqueDepartmentsAsync();
}