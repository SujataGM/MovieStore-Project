using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETAssignmentMovieStore.Models.ViewModels
{
    public class Order_Customers_Cart_View_Model
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DisplayName("Delivery Address")]
        public string DeliveryAddress { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DisplayName("Delivery City")]
        public string DeliveryCity { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        [DisplayName("Delivery Zip Code")]
        public string DeliveryZipCode { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Phone { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        [DisplayName("Billing Address")]
        public string BillingAddress { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        [Display(Name = "Billing Zip")]
        public string BillingZip { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DisplayName("Cart Id")]
        public int CartId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("Cart Quantity")]
        public int CartQuantity { get; set; }

        [Required]
        [DisplayName("Sub Total")]
        public decimal SubTotal { get; set; }
        [Required]
        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }

        public int OrderId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.Now;


        public string CoverImageURL { get; set; }

    }
}


