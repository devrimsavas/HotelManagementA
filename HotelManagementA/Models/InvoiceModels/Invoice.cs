using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagementA.Models.PaymentModels;
using HotelManagementA.Models.CustomerModels;


namespace HotelManagementA.Models.InvoiceModels
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }=Guid.NewGuid().ToString();

        //PAYMENT RELATION 
        [Required]
        public int PaymentId { get; set; }
        public Payment? Payment { get; set; }

        //CUSTOMER RELATION
        [Required]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        //INVOICE DETAILS 
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal TaxRate { get; set; } = 0.0m;

        [Required]
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public bool IsPaid { get; set; } = false;

    }
}
