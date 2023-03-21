using ASP.NETAssignmentMovieStore.Models;


namespace ASP.NETAssignmentMovieStore.Services.Movie
{
    public interface IMovieService
    {

        public void AddMovies(ASP.NETAssignmentMovieStore.Models.Movies m);

        public void EditMovies(int id, ASP.NETAssignmentMovieStore.Models.Movies m);

        public void DeleteMovies(int id, ASP.NETAssignmentMovieStore.Models.Movies m);

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetMoviesListofMoviesByID(int id);

        public ASP.NETAssignmentMovieStore.Models.Movies GetMoviesByID(int id);

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetAllMovies();       

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetLatestMovies();

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetOldestMovies();

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetCheapestMovies();
    }
}
