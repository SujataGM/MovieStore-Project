namespace ASP.NETAssignmentMovieStore.Models.ViewModels
{
    public class MostExpenciveOrderVM
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderSum { get; set; }
    }
}
