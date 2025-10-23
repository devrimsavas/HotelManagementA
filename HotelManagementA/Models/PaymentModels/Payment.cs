using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagementA.Models.CustomerModels;
using HotelManagementA.Models.HotelModels;

namespace HotelManagementA.Models.PaymentModels
{
    public enum PaymentType
    {
        CreditCard,
        Cash, 
        BankTransfer
    }
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        //RESERVATION 
        
        public int ReservationId { get; set; }
        
        public Reservation? Reservation { get; set; }

        //CUSTOMER 
        
        public int CustomerId { get; set; } 
        
        public Customer? Customer { get; set; }

        //PAYMENT TYPE 
        public PaymentType PaymentType { get; set; }=PaymentType.CreditCard;

        [Required]
        public decimal Amount { get; set; } 

        [Required]
        public DateTime PaymentDate { get; set; }

        public bool IsRefunded { get; set; } = false;


    }
}
