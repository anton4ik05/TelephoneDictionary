using Microsoft.AspNetCore.Mvc;
using TelephoneDictionary.Web.Models;
using TelephoneDictionary.Web.Repositories;

namespace TelephoneDictionary.Web.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController: ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IEnumerable<EmployeeDto> Get()
        {
            var employees = _employeeRepository.GetAll().Select(employees => employees.AsDto());
            return employees;
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeDto> GetById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);

            if (employee == null) return NotFound();

            return employee.AsDto();
        }

        [HttpGet("department/{id}")]
        public IEnumerable<EmployeeDto> GetByDepartmentId(int id)
        {
            var employees = _employeeRepository.GetAllByDepartmentId(id).Select(employees => employees.AsDto());
            return employees;
        }

        [HttpGet("search/{str}")]
        public IEnumerable<EmployeeDto> SearchEmployees(string str)
        {
            var employees = _employeeRepository.SearchEmployees(str).Select(employee => employee.AsDto());
            return employees;
        }

        [HttpPost]
        public ActionResult<EmployeeDto> Post(CreateEmployeeDto createEmployeeDto)
        {
            var employee = new Employee
            {
                LastName = createEmployeeDto.LastName,
                FirstName = createEmployeeDto.FirstName,
                Patronymic = createEmployeeDto.Patronymic,
                MobilePhone = createEmployeeDto.MobilePhone,
                Phone = createEmployeeDto.Phone,
                DepartmentId = createEmployeeDto.DepartmentId
            };
            _employeeRepository.CreateEmployee(employee);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = _employeeRepository.GetEmployeeById(id);

            if (employee == null) return NotFound();

            employee.LastName = updateEmployeeDto.LastName;
            employee.FirstName = updateEmployeeDto.FirstName;
            employee.Patronymic = updateEmployeeDto.Patronymic;
            employee.MobilePhone = updateEmployeeDto.MobilePhone;
            employee.Phone = updateEmployeeDto.Phone;
            employee.DepartmentId = updateEmployeeDto.DepartmentId;

            _employeeRepository.UpdateEmployee(employee);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);

            if(employee == null) return NotFound();

            _employeeRepository.RemoveEmployee(id);
            return NoContent();
        }


    }
}
