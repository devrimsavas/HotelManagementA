using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagementA.Models.CustomerModels;
using HotelManagementA.Models.StaffModels;

namespace HotelManagementA.Models.HotelModels
{
    public enum ReservationMadeBy
    {
        Customer ,
        Employee
        

    }
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid ReservationNumber { get; set; } = Guid.NewGuid();

        //hotel 
        [Required]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        //room 
        [Required] 
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        //customer 

        [Required]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        //reservation 
        [Required]
        
        public DateTime CheckInDate {  get; set; } 
        public DateTime CheckOutDate { get; set; }

        //reservation made by 
        public ReservationMadeBy WhoDidReservation {  get; set; }   

        //reservation status 
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }



    }
}
