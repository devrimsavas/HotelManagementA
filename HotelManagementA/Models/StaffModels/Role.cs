using System.ComponentModel.DataAnnotations;

namespace HotelManagementA.Models.StaffModels
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }=string.Empty;
        public List<Employee> Employees { get; set; } = new List<Employee>();


    }
}
