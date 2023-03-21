using ASP.NETAssignmentMovieStore.Data;
using ASP.NETAssignmentMovieStore.Models;
using ASP.NETAssignmentMovieStore.Models.ViewModels;
using ASP.NETAssignmentMovieStore.Services;
using ASP.NETAssignmentMovieStore.Services.Movie;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ASP.NETAssignmentMovieStore.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ApplicationDbContext _db;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly IOrderService _ordersService;
        private readonly IMovieService _movieService;
        public const string SessionKeyAddToCart = "Cart";
        public const string SessionKeyCartCount = "CartCount";
        public const string SessionCustID = "CustomerID";


        public CustomersController(ApplicationDbContext db,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            ICustomerService customerService, IOrderService orderService, IMovieService movieService)
        {
            _db = db;
            this.hostingEnvironment = hostingEnvironment;
            _customerService = customerService;
            _ordersService = orderService;
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            List<Customers> customersList = _customerService.GetCustomers();
            return View(customersList);
        }

        public IActionResult Details(int Id)
        {
            if (Id != null)
            {
                return View(_customerService.GetCustomer((int)Id));
            }

            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customers customer)
        {

            _customerService.CreateCustomer(customer);
            TempData["Success"] = "Customer Created Successfully !";
            return RedirectToAction("EmailValidation");
        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var custmfromdb = _db.Customers.Find(id);

            if (custmfromdb == null)
            {
                return NotFound();
            }
            return View(custmfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customers custmobj)
        {
            if (ModelState.IsValid)
            {
                _customerService.EditCustomer(custmobj);
                TempData["Success"] = "´Customer details Updated!";
                return RedirectToAction("Index");
            }

            return View(custmobj);
        }

        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var custmfromdb = _db.Customers.Find(id);

            if (custmfromdb == null)
            {
                return NotFound();
            }
            return View(custmfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            _customerService.DeleteCustomer(id);
            TempData["Success"] = "Customer Deleted Successfully !";
            return RedirectToAction("Index");
        }

        public IActionResult OrderDetails(int Id)
        {
            if (Id != null)
            {
                var order = _ordersService.GetOrder(Id);
                return View(order);
            }
            return View();
        }

        [HttpGet]
        public IActionResult EmailValidation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmailValidation(Customers customer)
        {
            Customers foundCustomer = (Customers)_db.Customers.Where(e => e.EmailAddress == customer.EmailAddress).FirstOrDefault();
            if (foundCustomer == null)
            {
                return RedirectToAction("Create");// Create a new Customer (Redirect)
            }
            else
            {
                return RedirectToAction("OrderSummary", new { email = foundCustomer.EmailAddress });
            }
        }

        public IActionResult OrderSummary(string email)
        {
            if (HttpContext.Session.Get<Cart>(SessionKeyAddToCart) == default)
            {
                HttpContext.Session.Set<Cart>(SessionKeyAddToCart, new Cart());
            }
            var cartDetails = HttpContext.Session.Get<Cart>(SessionKeyAddToCart);
            
            Customers custID = _db.Customers.Where(z => z.EmailAddress == email).FirstOrDefault();
            ViewBag.CustomerId = custID.Id;
            ViewBag.OrderDate = DateTime.Now;
            ViewBag.CustomerName = custID.FirstName + ' ' + custID.LastName;
            HttpContext.Session.Set<Customers>(SessionCustID, custID);
            if (cartDetails != null)
            {
                return View(cartDetails);
            }
            return View();
        }


        public IActionResult Checkout()
        {
            var cartDetails = HttpContext.Session.Get<Cart>(SessionKeyAddToCart);
            Customers custID = HttpContext.Session.Get<Customers>(SessionCustID);
            _ordersService.CreateOrdersAndOrderRows(cartDetails, custID);
            HttpContext.Session.Remove(SessionCustID);
            HttpContext.Session.Remove(SessionKeyAddToCart);
            HttpContext.Session.Remove(SessionKeyCartCount);
            return View("ThankYou");
        }

        [HttpGet]
        public IActionResult FindOrder(string EmailAddress)
        {
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                Customers custID = _db.Customers.Where(z => z.EmailAddress == EmailAddress).FirstOrDefault();
                if (custID == null)
                {
                    // TODO Maybe add tempdata here?
                    TempData["Test"] = "";
                    return View();
                }
                var findOrderId = _db.Orders.Where(k => k.CustomerId == custID.Id)
                                   .Join(_db.OrderRows,
                                     k => k.OrderId,
                                     orderRows => orderRows.OrdersId,
                                     (k, OrderRows) => new ListOfOrdersVM
                                     {
                                         EmailAddress = custID.EmailAddress,
                                         OrderId = k.OrderId,
                                         OrderDate = k.OrderDate,
                                         MovieId = OrderRows.MoviesId,
                                         Title = OrderRows.Movies.Title,
                                         Subtotal = OrderRows.Price,
                                         ImageUrl = OrderRows.Movies.CoverImageURL
                                     }).ToList();
                ViewBag.EmailAddress = EmailAddress;
                return View(findOrderId);
            }
            else
            {
                return View();
            }
        }

        public IActionResult MostExpOrder()
        {
            var order = (Orders)_db.Orders.
                OrderByDescending(p => p.OrderRows.
                Sum(r => r.Price)).FirstOrDefault();

            if (order != null)
            {
                var customer = _db.Customers.Find(order.CustomerId);

                MostExpenciveOrderVM expensiveOrder = new MostExpenciveOrderVM()
                {
                    CustomerName = customer.FirstName + " " + customer.LastName,
                    OrderDate = order.OrderDate,
                    OrderSum = order.OrderRows.Sum(p => p.Price),
                };


                return View(expensiveOrder);
            }
            else
            {
                return View();
            }
        }



    }

}
