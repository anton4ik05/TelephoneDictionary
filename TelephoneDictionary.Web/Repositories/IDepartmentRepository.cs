using TelephoneDictionary.Web.Models;

namespace TelephoneDictionary.Web.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        IEnumerable<Department> GetAllChildDepartments(int id);
        Department GetDepartmentById(int id);
        void CreateDepartment(Department department, int id);
        void UpdateDepartment(Department department);
        void RemoveDepartment(int id);
    }
}
