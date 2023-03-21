namespace ASP.NETAssignmentMovieStore.Models.ViewModels
{
    public class SessionCart
    {
        public Guid CartGuid { get; set; }

        public int CardId { get; set; }
        public int MoviesId { get; set; }
        public virtual Movies Movies { get; set; }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public string ImageUrl { get; set; }


        public SessionCart(int moviesId, string title, decimal price, int quantity, string imageUrl)
        {
            MoviesId = moviesId;
            Title = title;
            Price = price;
            Quantity = quantity;
            SubTotal = price * quantity;   
            ImageUrl = imageUrl;
        }

        
        public decimal Total 
        {
            get
            {
                return Price * Quantity;
            }
        }
    }

}
