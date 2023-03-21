namespace ASP.NETAssignmentMovieStore.Models.ViewModels
{
    public class ListOfOrdersVM
    {
        public string EmailAddress { get; set; }

        public string Title { get; set; }

        public int MovieId { get; set; }

        public string ImageUrl { get; set; }

        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public double Subtotal { get; set; }

        public decimal Total { get; set; }
    }
}
