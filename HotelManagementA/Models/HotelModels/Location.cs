using System.ComponentModel.DataAnnotations;
namespace HotelManagementA.Models.HotelModels
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }=string.Empty;
        public string PostCode { get; set; }= string.Empty;
        [Required]
        [StringLength(250)]
        public string Street {  get; set; }=string.Empty;   
        [Required]
        public string Country { get; set; } =string.Empty;


    }
}
