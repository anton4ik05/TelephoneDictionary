using Microsoft.AspNetCore.Mvc;
using TelephoneDictionary.Web.Models;
using TelephoneDictionary.Web.Repositories;

namespace TelephoneDictionary.Web.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController: ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IEnumerable<DepartmentDto> Get()
        {
            var departments = _departmentRepository.GetAll().Select(department => department.AsDto());
            return departments;
        }

        [HttpGet("childbranch/{id}")]
        public IEnumerable<DepartmentDto> GetChildById(int id)
        {
            var departments = _departmentRepository.GetAllChildDepartments(id).Select(department => department.AsDto());
            return departments;
        }

        [HttpGet("{id}")]
        public ActionResult<DepartmentDto> GetById(int id)
        {
            var department = _departmentRepository.GetDepartmentById(id);

            if (department == null) return NotFound();

            return department.AsDto();
        }

        [HttpPost("{id}")]
        public ActionResult<DepartmentDto> Post(CreateDepartmentDto createDepartmentDto, int id)
        {
            var department = new Department
            {
                Name = createDepartmentDto.Name
            };

            _departmentRepository.CreateDepartment(department,id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateDepartmentDto updateDepartmentDto)
        {
            var department = _departmentRepository.GetDepartmentById(id);

            if (department == null) return NotFound();

            department.Name = updateDepartmentDto.Name;

            _departmentRepository.UpdateDepartment(department);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var department = _departmentRepository.GetDepartmentById(id);

            if (department == null) return NotFound();


            _departmentRepository.RemoveDepartment(id);
           return NoContent();
        }



    }
}
