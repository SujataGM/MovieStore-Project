using ASP.NETAssignmentMovieStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETAssignmentMovieStore.Models
{
    public class Cart
    {
        public List<SessionCart> Movieids { get; set; }


        public Cart()
        {
            Movieids = new List<SessionCart>();
        }
       
    }
    
}
