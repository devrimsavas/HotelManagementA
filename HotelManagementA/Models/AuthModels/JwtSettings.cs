namespace HotelManagementA.Models.AuthModels
{
    public class JwtSettings
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string Audience { get; set; } = string.Empty;
        public int ExpiryMinutes { get; set; }
    }
}
