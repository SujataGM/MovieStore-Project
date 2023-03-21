using ASP.NETAssignmentMovieStore.Service;
using ASP.NETAssignmentMovieStore.Helper;
using Microsoft.AspNetCore.Mvc;
using ASP.NETAssignmentMovieStore.Models;
using ASP.NETAssignmentMovieStore.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ASP.NETAssignmentMovieStore.Services.Movie;
using ASP.NETAssignmentMovieStore.Models.ViewModels;

namespace ASP.NETAssignmentMovieStore.Controllers
{
    public class CartController : Controller
    {
        public readonly ApplicationDbContext _db;
        
        
        private readonly IMovieService _movieService;
        const string SessionKeyAddToCart = "AddToCart";
      
         
        public CartController( ApplicationDbContext db,IMovieService movieService)
        {
           
            
            _db = db;
            _movieService = movieService;
        }
        public IActionResult Index()
        {


            List<Movies> movielist = _movieService.GetAllMovies().ToList();


            return View(movielist);
        }
        public IActionResult AddToTheCart()
        {


            return View();

        }
        [HttpPost]
        public IActionResult AddToTheCart(int Id)
        {
            


            return RedirectToAction("Index");

        }
        

           


    }
}

