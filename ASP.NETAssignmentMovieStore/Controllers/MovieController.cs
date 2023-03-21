using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP.NETAssignmentMovieStore.Helper;
using ASP.NETAssignmentMovieStore.Models;
using ASP.NETAssignmentMovieStore.Models.ViewModels;
using ASP.NETAssignmentMovieStore.Services;
using ASP.NETAssignmentMovieStore.Data;
using ASP.NETAssignmentMovieStore.Services.Movie;
using ASP.NETAssignmentMovieStore.Service;
using ASP.NETAssignmentMovieStore;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;

using PagedList;
using PagedList.Mvc;


#nullable disable
namespace ASP.NETAssignmentMovieStore.Controllers
{
    public class MovieController : Controller
    {

        private readonly IMovieService _movieService;
        

        private readonly ApplicationDbContext _db;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        public const string SessionKeyAddToCart = "Cart";
        public const string SessionKeyCartCount = "CartCount";

        public MovieController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            ApplicationDbContext db,

            IMovieService movieService)

        {
            _db = db;
            this.hostingEnvironment = hostingEnvironment;
            _movieService = movieService;


        }
        // GET: MoviesController
        public IActionResult Index()
        {
            List<Movies> listofMovies = _movieService.GetAllMovies();
            return View(listofMovies);
        }

        public async Task<IActionResult> ShowView()
        {
            if (HttpContext.Session.Get<Cart>(SessionKeyAddToCart) == default)
            {
                HttpContext.Session.Set<Cart>(SessionKeyAddToCart, new Cart());
            }

            var sesstionObject = HttpContext.Session.Get<Cart>(SessionKeyAddToCart);

            return View(sesstionObject);
        }      

        public IActionResult AddToCart(int Id)
        {
            if (HttpContext.Session.Get<Cart>(SessionKeyAddToCart) == default)
            {
                HttpContext.Session.Set<Cart>(SessionKeyAddToCart, new Cart());
            }
            var sesstionObject = HttpContext.Session.Get<Cart>(SessionKeyAddToCart);
            List<Movies> moviedetails = _movieService.GetMoviesListofMoviesByID(Id);
            var movie = _db.Movies.Find(Id);
            if (movie != null)
            {
                var addacart = sesstionObject.Movieids.FirstOrDefault(m => m.MoviesId == Id);
                if (addacart != null)
                {
                    ++addacart.Quantity;
                    addacart.SubTotal = addacart.Price * addacart.Quantity;
                }
                else
                {
                    sesstionObject.Movieids.Add(new SessionCart(movie.Id, movie.Title, Convert.ToDecimal(movie.Price), 1, movie.CoverImageURL));                   

                }
            }
            if (moviedetails.Count > 0)
            {
                int count = moviedetails.Count;
                if (Convert.ToInt32(HttpContext.Session.GetString(SessionKeyCartCount)) != 0)
                {
                    count = Convert.ToInt32(HttpContext.Session.GetString(SessionKeyCartCount)) + count;
                    HttpContext.Session.SetString(SessionKeyCartCount, (sesstionObject.Movieids.Sum(item => item.Quantity)).ToString());
                }
                else
                {
                    HttpContext.Session.SetString(SessionKeyCartCount, (sesstionObject.Movieids.Sum(item => item.Quantity)).ToString());
                }
            }

            HttpContext.Session.Set<Cart>(SessionKeyAddToCart, sesstionObject);

            return RedirectToAction("ShowView");

            

        }
        

        public IActionResult CartItems()
        {
            return Redirect("ShowView");
        }

        public IActionResult RemoveQuantity(int Id)
        {
            if (HttpContext.Session.Get<Cart>(SessionKeyAddToCart) == default)
            {
                HttpContext.Session.Set<Cart>(SessionKeyAddToCart, new Cart());
            }
            var sesstionObject = HttpContext.Session.Get<Cart>(SessionKeyAddToCart);
            var sessionCartCount = HttpContext.Session.GetString(SessionKeyCartCount);
            var movie = _db.Movies.Find(Id);
            var removecart = sesstionObject.Movieids.Find(m => m.MoviesId == Id);
            if (removecart != null)
            {
                --removecart.Quantity;
                removecart.SubTotal = removecart.Price * removecart.Quantity;
              
                HttpContext.Session.Set<Cart>(SessionKeyAddToCart, sesstionObject);
                HttpContext.Session.SetString(SessionKeyCartCount, (sesstionObject.Movieids.Sum(item => item.Quantity)).ToString());
            }
            

            return RedirectToAction("ShowView");  
        }

        public IActionResult AddQuantity(int Id)
        {
            if (HttpContext.Session.Get<Cart>(SessionKeyAddToCart) == default)
            {
                HttpContext.Session.Set<Cart>(SessionKeyAddToCart, new Cart());
            }
            var sesstionObject = HttpContext.Session.Get<Cart>(SessionKeyAddToCart);
            var sessionCartCount = HttpContext.Session.GetString(SessionKeyCartCount);
            var movie = _db.Movies.Find(Id);
            var addcart = sesstionObject.Movieids.Find(m => m.MoviesId == Id);
            if (addcart != null)
            {
                ++addcart.Quantity;
                addcart.SubTotal = addcart.Price * addcart.Quantity;
            }
            HttpContext.Session.Set<Cart>(SessionKeyAddToCart, sesstionObject);
            HttpContext.Session.SetString(SessionKeyCartCount, (sesstionObject.Movieids.Sum(item =>item.Quantity)).ToString());

            return RedirectToAction("ShowView");
        }
        public IActionResult DeleteFromTheCart(int Id)
        {
            if (HttpContext.Session.Get<Cart>(SessionKeyAddToCart) == default)
            {
                HttpContext.Session.Set<Cart>(SessionKeyAddToCart, new Cart());
            }

            var sesstionObject = HttpContext.Session.Get<Cart>(SessionKeyAddToCart);
            var movie = _db.Movies.Find(Id);
            if (movie != null)
            {
                var addacart = sesstionObject.Movieids.FirstOrDefault(m => m.MoviesId == Id);
                if (addacart != null)
                {                    
                    sesstionObject.Movieids.RemoveAll(c => c.MoviesId == Id);
                    HttpContext.Session.SetString(SessionKeyCartCount, (sesstionObject.Movieids.Sum(item => item.Quantity)).ToString());
                }
                

            }

            HttpContext.Session.Set<Cart>(SessionKeyAddToCart, sesstionObject);
            return RedirectToAction("ShowView");
        }
                
        public IActionResult SearchMovie(string searchString)
        {
            ViewData["GetMovieDetails"] = searchString;


            var movies = from p in _db.Movies select p;
            {

                if (!string.IsNullOrEmpty(searchString))
                {
                    //movies= movies.Where(s=>s.Title == searchString);
                    movies = movies.Where(s => s.Title.Contains(searchString)
                    || s.Director.Contains(searchString) || s.Category.Contains(searchString) ||
                    s.Price.ToString().Contains(searchString) ||
                    s.ReleaseYear.ToString().Contains(searchString));
                }


            }

            return View(movies);
        }

        




        // GET: MoviesController/Details/5
        public IActionResult Details(int id)
        {
            if (id != null)
            {
                var list = _movieService.GetMoviesByID(id);
                return View(list);
            }
            return View();
        }

        // GET: MoviesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MoviesCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;
                    if (model.CoverMovieImage != null)
                    {

                        string uploadFilesFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CoverMovieImage.FileName;
                        string filePath = Path.Combine(uploadFilesFolder, uniqueFileName);
                        model.CoverMovieImage.CopyTo(new FileStream(filePath, FileMode.Create));
                    }

                    Movies newMovie = new Movies
                    {
                        Title = model.Title,
                        Director = model.Director,
                        ReleaseYear = model.ReleaseYear,
                        Price = model.Price,
                        Description = model.Description,
                        Category = model.Category,
                        MovieTime = model.MovieTime,
                        CoverImageURL = "images/" + uniqueFileName

                    };
                    if (newMovie != null)
                    {
                        _movieService.AddMovies(newMovie);
                    }
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: MoviesController/Edit/5
        public IActionResult Edit(int id)
        {
            if (id != null)
            {
                Movies editMovie = _movieService.GetMoviesByID(id);
                MoviesEditViewModel movieEdit = new MoviesEditViewModel
                {
                    Id = editMovie.Id,
                    Title = editMovie.Title,
                    Director = editMovie.Director,
                    ReleaseYear = editMovie.ReleaseYear,
                    Price = editMovie.Price,
                    Description = editMovie.Description,
                    Category = editMovie.Category,
                    MovieTime = editMovie.MovieTime,
                    ExistingCoverImageURL = editMovie.CoverImageURL
                };
                return View(movieEdit);
            }
            return View();
        }

        // POST: MoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MoviesEditViewModel model)
        {
            try
            {
                Movies m = _movieService.GetMoviesByID(model.Id);
                m.Id = model.Id;
                m.Title = model.Title;
                m.Director = model.Director;
                m.ReleaseYear = model.ReleaseYear;
                m.Price = model.Price;
                m.Description = model.Description;
                m.Category = model.Category;
                m.MovieTime = model.MovieTime;

                string uniqueFileName = null;
                if (model.CoverMovieImage != null)
                {
                    var currentImagePath = Path.Combine(hostingEnvironment.WebRootPath, m.CoverImageURL);
                    if (System.IO.File.Exists(currentImagePath))
                    {
                        System.IO.File.Delete(currentImagePath);
                    }

                    string uploadFilesFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CoverMovieImage.FileName;
                    string filePath = Path.Combine(uploadFilesFolder, uniqueFileName);
                    model.CoverMovieImage.CopyTo(new FileStream(filePath, FileMode.Create));
                    m.CoverImageURL = "images/" + uniqueFileName;
                }
                _movieService.EditMovies(model.Id, m);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: MoviesController/Delete/5
        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                var editMovie = _movieService.GetMoviesByID(id);
                return View(editMovie);
            }
            return View();
        }

        // POST: MoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Movies m)
        {
            try
            {
                if (id != null)
                {
                    var deleteMovie = _movieService.GetMoviesByID(id);
                    _movieService.DeleteMovies(id, deleteMovie);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }


       

    }
}
