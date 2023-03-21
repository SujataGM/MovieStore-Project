using System.ComponentModel.DataAnnotations;

namespace ASP.NETAssignmentMovieStore.Models.ViewModels
{
    public class Customer_Order_Cart_VM :Cart
    {
        public int OrderId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<SessionCart> Movieids { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public virtual Customers Customers { get; set; }

        public Customer_Order_Cart_VM()
        {

        }

        public Customer_Order_Cart_VM(int orderID, DateTime orderDate, List<SessionCart> moviesIds)
        {
            OrderId = orderID;
            OrderDate = DateTime.Now;
            Movieids = moviesIds;
        }

    }
}
