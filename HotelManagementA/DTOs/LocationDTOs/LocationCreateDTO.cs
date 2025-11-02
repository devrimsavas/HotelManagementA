namespace HotelManagementA.DTOs.LocationDTOs
{
    public class LocationCreateDTO
    {
        public string City { get; set; }=string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public string Street {  get; set; } = string.Empty; 
        public string Country {  get; set; } = string.Empty;
    }
}
