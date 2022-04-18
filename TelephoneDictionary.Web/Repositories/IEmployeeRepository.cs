using TelephoneDictionary.Web.Models;

namespace TelephoneDictionary.Web.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetEmployeeById(int id);
        IEnumerable<Employee> GetAllByDepartmentId(int id);

        IEnumerable<Employee> SearchEmployees(string str);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void RemoveEmployee(int id);

    }
}
