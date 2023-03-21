namespace ASP.NETAssignmentMovieStore.Models.ViewModels
{
    public class CartViewModel :Cart
    {
        public List<int> MovieIds { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public CartViewModel()
        {
            MovieIds = new List<int>();
        }
    }
}
