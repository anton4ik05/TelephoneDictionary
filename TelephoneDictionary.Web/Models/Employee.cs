using System.ComponentModel.DataAnnotations;

namespace TelephoneDictionary.Web.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
        public string Phone { get; set; } = null!;
        
        [Range(1,1000)]
        public int DepartmentId { get; set; }
    }
}
