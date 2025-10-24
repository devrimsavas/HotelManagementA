using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagementA.Models.CustomerModels;


namespace HotelManagementA.Models.HotelModels
{
    public class HotelReview
    {
        [Key]
        public int Id { get; set; }

        public int Rating { get; set; } = 0;

        //hotel 
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        //customer 
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Column(TypeName ="TEXT")]
        public  string Review  { get; set; }=string.Empty;

        public DateTime? ReviewDate { get; set; }

    }
}
