using System.ComponentModel.DataAnnotations;

namespace HotelManagementA.DTOs.AuthDTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }=string.Empty;
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } =string.Empty;
        [Required]
        public string Email { get; set; }= string.Empty;
        [Required]
        [StringLength(20,MinimumLength =6)]
        public string Password { get; set; } =string.Empty; 

        [Required]
        public int RoleId { get; set; }
        [Required]
        public int HotelId { get; set; }
    }
}
