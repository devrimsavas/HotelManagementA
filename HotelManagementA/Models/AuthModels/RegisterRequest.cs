using System.ComponentModel.DataAnnotations;

namespace HotelManagementA.Models.AuthModels
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }     

        [Required]
        public int HotelId { get; set; }    

    }
}
