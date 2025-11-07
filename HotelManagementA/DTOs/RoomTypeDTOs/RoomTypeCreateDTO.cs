using System.ComponentModel.DataAnnotations;
namespace HotelManagementA.DTOs.RoomTypeDTOs
{
    public class RoomTypeCreateDTO
    {
        [Required]
        public string Name { get; set; }=string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.0,double.MaxValue)]
        public decimal Price { get; set; }

        public int Capacity { get; set; }
    }
}
