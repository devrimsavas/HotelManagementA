using HotelManagementA.Models.StaffModels;
using System.ComponentModel.DataAnnotations;
namespace HotelManagementA.Models.HotelModels
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        [Required]
        public string Telephone {  get; set; } = string.Empty;
        public List<Room> Rooms { get; set; }= new List<Room>();

        public Location Location { get; set; }  =new Location();
        public int LocationId { get; set; }

        public List<Employee> Employees { get; set;} = new List<Employee>();
        


    }
}
