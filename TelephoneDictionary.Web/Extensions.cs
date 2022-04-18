using TelephoneDictionary.Web.Models;

namespace TelephoneDictionary.Web
{
    public static class Extensions
    {
        public static DepartmentDto AsDto(this Department department)
        {
            return new DepartmentDto(department.Id, department.Name, department.Path);
        }

        public static EmployeeDto AsDto(this Employee employee)
        {
            return new EmployeeDto(employee.Id, employee.LastName, employee.FirstName, employee.Patronymic,
                employee.MobilePhone, employee.Phone, employee.DepartmentId);
        }
    }
}
