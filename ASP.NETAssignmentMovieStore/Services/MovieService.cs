using ASP.NETAssignmentMovieStore.Data;
using ASP.NETAssignmentMovieStore.Models;
using ASP.NETAssignmentMovieStore.Models.ViewModels;
using ASP.NETAssignmentMovieStore.Services.Movie;
using System.Collections.Generic;


namespace ASP.NETAssignmentMovieStore.Services.Movies
{
    public class MovieService : IMovieService
    {

        public readonly ApplicationDbContext _db;

        public MovieService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AddMovies(ASP.NETAssignmentMovieStore.Models.Movies m)
        {
            _db.Movies.Add(m);
            _db.SaveChanges();
        }

        public void DeleteMovies(int movieid, ASP.NETAssignmentMovieStore.Models.Movies m)
        {
            var id = _db.Movies.Find(movieid);
            if (id != null)
            {
                _db.Movies.Remove(m);
                _db.SaveChanges();
            }
        }

        public void EditMovies(int movieid, ASP.NETAssignmentMovieStore.Models.Movies m)
        {
            var id = _db.Movies.Find(movieid);
            if (id != null)
            {
                _db.Movies.Update(m);
                _db.SaveChanges();
            }

        }

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetMoviesListofMoviesByID(int id)
        {
            List<ASP.NETAssignmentMovieStore.Models.Movies> listMovies = _db.Movies.Where(x => x.Id == id).ToList();
            return listMovies;
        }

        public ASP.NETAssignmentMovieStore.Models.Movies GetMoviesByID(int id)
        {
            ASP.NETAssignmentMovieStore.Models.Movies movie = _db.Movies.Where(x => x.Id == id).FirstOrDefault();
            return movie;
        }

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetAllMovies()
        {
            List<ASP.NETAssignmentMovieStore.Models.Movies> listOfAllMovies = _db.Movies.ToList();
            return listOfAllMovies;
        }
      

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetLatestMovies()
        {
            List<ASP.NETAssignmentMovieStore.Models.Movies> top5LatestMovies = _db.Movies.OrderByDescending(x => x.ReleaseYear).Take(5).ToList();
            return top5LatestMovies;
        }

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetOldestMovies()
        {
            List<ASP.NETAssignmentMovieStore.Models.Movies> oldestMovies = _db.Movies.OrderBy(x => x.ReleaseYear).Take(5).ToList();
            return oldestMovies;
        }

        public List<ASP.NETAssignmentMovieStore.Models.Movies> GetCheapestMovies()
        {
            List<ASP.NETAssignmentMovieStore.Models.Movies> cheapestMovies = _db.Movies.OrderBy(x => x.Price).Take(5).ToList();
            return cheapestMovies;
        }

        



    }
}
