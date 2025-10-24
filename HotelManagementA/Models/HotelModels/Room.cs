using System.ComponentModel.DataAnnotations;
namespace HotelManagementA.Models.HotelModels
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }

        public Hotel? Hotel { get; set; }
        public int HotelId { get; set; }
        //CLEAN ?
        public bool IsCleaned { get; set; } = false;

       



    }
}
