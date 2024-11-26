namespace Domain.Dtos
{
    public class DepartmentMatrixDto
    {
        public string Name { get; set; }
        public List<UserMatrixDto> Users { get; set; }

        public DepartmentMatrixDto(string name, List<UserMatrixDto> users)
        {
            Name = name;
            Users = users;
        }

        public DepartmentMatrixDto() 
        {
            Users = new List<UserMatrixDto>();
        }
    }
}