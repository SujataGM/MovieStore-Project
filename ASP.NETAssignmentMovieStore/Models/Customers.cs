#nullable disable
using System.ComponentModel.DataAnnotations;
namespace ASP.NETAssignmentMovieStore.Models
{
    public class Customers
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(250, MinimumLength = 4)]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Billing Zip")]
        public string BillingZip { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; }
        
        [Required]
        [StringLength(250, MinimumLength = 4)]
        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Delivery Zip")]
        public string DeliveryZip { get; set; }
       
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = "Delivery City")]
        public string DeliveryCity { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Phone No")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
    }  
}

