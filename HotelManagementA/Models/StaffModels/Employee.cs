using HotelManagementA.Models.HotelModels;
using System.ComponentModel.DataAnnotations;


namespace HotelManagementA.Models.StaffModels
{
    public enum Roles
    {
        Admin,
        Manager,
        Receptionist,
        Kitchen,
        Cleaner,
        CommonWorker, //defined ones 
        Customer

    }
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string PersonalCode { get; set; }= string.Empty;
        [Required]
        public string Email { get; set; }=string.Empty;
        [Required]
        //role 
        public Roles role { get; set; } = Roles.CommonWorker;

        //Relations to Hotel 
        [Required]
        public int HotelId { get; set; }
        [Required]
        public Hotel? Hotel { get; set; }

    }
}
