using ASP.NETAssignmentMovieStore.Data;
using ASP.NETAssignmentMovieStore.Data.Migrations;
using ASP.NETAssignmentMovieStore.Models;
using ASP.NETAssignmentMovieStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETAssignmentMovieStore.ViewComponents
{
    public class ExpensiveCustomersViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public ExpensiveCustomersViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }


        public IViewComponentResult Invoke()
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
                    OrderSum = order.OrderRows.Sum(p => p.Price)
                };
                return View(expensiveOrder);

            }
            return Content(string.Empty);
        }
    }
}
