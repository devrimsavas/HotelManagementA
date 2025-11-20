namespace HotelManagementA.DTOs.StaffDTOs
{
    public class EmloyeeCreateDTO
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } =string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int HotelId { get; set; }
    }
}
