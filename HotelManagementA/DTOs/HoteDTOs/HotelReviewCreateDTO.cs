namespace HotelManagementA.DTOs.HoteDTOs
{
    public class HotelReviewCreateDTO
    {
        public int Rating { get; set; }
        public int HotelId { get; set; }
        public int CustomerId { get; set; }
        public string Review { get; set; } = string.Empty;

       


    }
}
