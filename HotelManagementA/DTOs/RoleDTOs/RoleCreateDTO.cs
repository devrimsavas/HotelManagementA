using System.ComponentModel.DataAnnotations;
namespace HotelManagementA.DTOs.RoleDTOs
{
    public class RoleCreateDTO
    {
        

        [Required]
        public string Type {get; set; }=string.Empty;


    }
}
