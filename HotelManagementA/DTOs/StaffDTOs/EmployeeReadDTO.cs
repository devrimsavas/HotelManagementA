namespace HotelManagementA.DTOs.StaffDTOs
{
    public class EmployeeReadDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email {  get; set; }= string.Empty;
        public string Role { get; set; } = string.Empty;
        public string HotelName {  get; set; } = string.Empty;

    }
}
