namespace TelephoneDictionary.Web
{
    public record EmployeeDto(int Id, string LastName, string FirstName, string Patronymic, string MobilePhone,
        string Phone, int DepartmentId);

    public record CreateEmployeeDto(string LastName, string FirstName, string Patronymic, string MobilePhone,
        string Phone, int DepartmentId);

    public record UpdateEmployeeDto(string LastName, string FirstName, string Patronymic, string MobilePhone,
        string Phone, int DepartmentId);

    public record DepartmentDto(int Id, string Name, int[] Path);

    public record CreateDepartmentDto(string Name);

    public record UpdateDepartmentDto(string Name);
}
