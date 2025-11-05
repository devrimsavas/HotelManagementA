using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementA.Models.HotelModels
{
    public class RoomType
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]    
        public string Description { get; set; } = "Generic Description";
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        public int Capacity { get; set; }

        public List<Room> Rooms { get; set; }= new List<Room>(); //navigation 




    }
}
