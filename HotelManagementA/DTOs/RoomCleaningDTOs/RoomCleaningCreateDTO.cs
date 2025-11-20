namespace HotelManagementA.DTOs.RoomCleaningDTOs
{
    public class RoomCleaningCreateDTO
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int CleanerId {  get; set; }
        public bool IsCleaned { get; set; }
        public DateTime? CleanedAt { get; set; }
    }
}
