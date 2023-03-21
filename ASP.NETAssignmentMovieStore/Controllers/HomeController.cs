using ASP.NETAssignmentMovieStore.Data;
using ASP.NETAssignmentMovieStore.Models;
using ASP.NETAssignmentMovieStore.Models.ViewModels;
using ASP.NETAssignmentMovieStore.Services;
using ASP.NETAssignmentMovieStore.Services.Movie;
using Microsoft.AspNetCore.Mvc;
using System.Data.OracleClient;
using System.Diagnostics;

namespace ASP.NETAssignmentMovieStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly  IOrderService _orderService;
        private readonly ApplicationDbContext _db;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly ILogger<HomeController> _logger;
       

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
             IMovieService movieService, IOrderService orderService)
        {
            _db = db;
            this.hostingEnvironment = hostingEnvironment;
            _movieService = movieService;
            _logger = logger;
            _orderService = orderService;
          
            
        }

        public IActionResult Index()
        {
            List<Movies> listofnewMovies = _movieService.GetAllMovies();
            return View(listofnewMovies);
        } 

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}