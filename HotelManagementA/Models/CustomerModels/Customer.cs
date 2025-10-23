using HotelManagementA.Models.HotelModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HotelManagementA.Models.CustomerModels
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } =string.Empty;
        [EmailAddress]
        public string Email { get; set; }=string.Empty;
        [Required]
        public string PhoneNumber { get; set; }=string.Empty;

        //reservations 
        public List<Reservation> Reservations { get; set; }=new List<Reservation>();







    }
}
