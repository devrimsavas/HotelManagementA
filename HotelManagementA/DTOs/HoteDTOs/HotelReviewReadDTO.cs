using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementA.DTOs.HoteDTOs
{
    public class HotelReviewReadDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; } 
        public string HotelName { get; set; }=string.Empty;
        public string CustomerName {  get; set; }=string.Empty;
        public string Review {  get; set; }=string.Empty;

        public DateTime? ReviewDate { get; set; }
        
    }
}
