namespace HotelManagementA.DTOs.HoteDTOs
{
    public class HotelReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;

        public string City {  get; set; } = string.Empty;

        public double AverageRating { get; set; } = 0;



    }
}
