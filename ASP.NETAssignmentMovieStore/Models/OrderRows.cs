using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETAssignmentMovieStore.Models
{
    public class OrderRows
    {
        public int Id { get; set; }

        public int OrdersId { get; set; }
        public virtual Orders Orders { get; set; }
        public int  MoviesId { get; set; }
        public virtual Movies Movies { get; set; }

        [Required]
        [StringLength(600, MinimumLength = 3)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public OrderRows()
        {

        }

        //public OrderRows(int moviesId, double price)
        //{            
        //    MoviesId = moviesId;           
        //    Price = price;
        //}
    }
}
