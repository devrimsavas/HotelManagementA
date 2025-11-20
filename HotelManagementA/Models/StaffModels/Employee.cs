using HotelManagementA.Models.HotelModels;
using System.ComponentModel.DataAnnotations;


namespace HotelManagementA.Models.StaffModels
{
    
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }= string.Empty;
        [Required]
        public string Email { get; set; }=string.Empty;
        [Required]
        //role navigation 
        public int RoleId { get; set; }
        public Role? Role { get; set; }


        //PASSWORD 
        public string? PasswordHash { get; set; }

        //Relations to Hotel 
        [Required]
        public int HotelId { get; set; }
        [Required]
        public Hotel? Hotel { get; set; }

    }
}
