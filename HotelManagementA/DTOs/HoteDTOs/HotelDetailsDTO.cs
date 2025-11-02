using HotelManagementA.DTOs.RoomDTOs;


namespace HotelManagementA.DTOs.HoteDTOs
{
    public class HotelDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public double AverageRating { get; set; } = 0;
        public int TotalReviews { get; set; } = 0;

        public List<RoomReadDTO> Rooms { get; set; } = new();
        public List<HotelReviewReadDTO> Reviews  { get; set;} = new();  
        
    }
}
