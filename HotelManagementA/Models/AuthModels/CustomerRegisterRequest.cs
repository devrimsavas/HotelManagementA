namespace HotelManagementA.Models.AuthModels
{
    public class CustomerRegisterRequest
    {
        public string FirsName { get; set; }=string.Empty;
        public string LastName {  get; set; }=string.Empty;
        public string Email { get; set; } =string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

    }
}
