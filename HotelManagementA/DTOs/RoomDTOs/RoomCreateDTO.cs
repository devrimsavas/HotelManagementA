namespace HotelManagementA.DTOs.RoomDTOs
{
    public class RoomCreateDTO
    {
        
        public int RoomNumber { get; set; }
        public int  RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public bool IsCleaned { get; set; } = false;
    }
}
