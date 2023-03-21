using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;


#nullable disable
namespace ASP.NETAssignmentMovieStore.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public int CustomerId { get; set; }

        public virtual Customers Customers { get; set; }

        //public int OrderRowsId { get; set; }

        public virtual ICollection<OrderRows> OrderRows { get; set; }
        //public object Price { get; internal set; }

        public Orders()
        {
            OrderRows = new List<OrderRows>();
        }

       
    }
}
