namespace TelephoneDictionary.Web.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int[] Path { get; set; } = null!;
    }
}
