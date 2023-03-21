using ASP.NETAssignmentMovieStore.Data;
using ASP.NETAssignmentMovieStore.Models;
using ASP.NETAssignmentMovieStore.Services.Movie;
using ASP.NETAssignmentMovieStore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using ASP.NETAssignmentMovieStore.Helper;
using static NuGet.Packaging.PackagingConstants;




namespace ASP.NETAssignmentMovieStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;  
        public const string SessionKeyAddToCart = "Cart";
        public const string SessionKeyCartCount = "CartCount";
        public const string SessionCustID = "CustomerID";
        public OrderService(ApplicationDbContext db)
        {
            _db = db;           
        }

        /// <summary>
        /// Get a list of orders made by one customer
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public List<Orders> GetOrders(int CustomerId)
        {
            List<Orders> orders = _db.Orders.Where(o => o.CustomerId == CustomerId).ToList();

            return orders;
        }

        /// <summary>
        /// Get details for one order (list all movies orderd)
        /// </summary>
        /// <param /*name="Id"*/></param>
        /// <returns></returns>
        public Orders GetOrder(int Id)
        {
            return _db.Orders.Find(Id);
        }
        
        public void CreatOrder(Orders o)
        {
            _db.Orders.Add(o);
            _db.SaveChanges();
        }

        public void CreatOrderRows(List<OrderRows> orderRows)
        {
            _db.OrderRows.AddRange(orderRows);
            _db.SaveChanges();
        }
        
        public void CreateOrdersAndOrderRows(Cart cartitems, Customers custid) 
        {
            Orders order = new Orders();
            order.CustomerId = custid.Id;        
            order.OrderDate = DateTime.Now;   
            foreach (var item in cartitems.Movieids)
            {
                OrderRows newRow = new()
                {
                    MoviesId = item.MoviesId,
                    Price = Convert.ToDouble(item.Price)
                    //Price = _db.Movies.Find(item).Price
                };
                order.OrderRows.Add(newRow);
            }
            _db.Orders.Add(order);
            _db.SaveChanges();

            
            //Orders newOrder = new Orders
            //{
            //    CustomerId = (int)custid,
            //    OrderDate = DateTime.Now,                
            //};
            //_ordersService.CreatOrder(newOrder);
            //Orders orderDts = _ordersService.GetOrder((int)custid);
            //var orderRows = new List<OrderRows>();
            //foreach (var movie in cartitems.Movieids)
            //{
            //    for (int i = 0; i < movie.Quantity - 1; i++)
            //    {
            //        OrderRows neworderrow = new OrderRows
            //        {
            //            OrdersId = orderDts.OrderId,
            //            MoviesId = movie.MoviesId,
            //            Price = (double)movie.Price
            //        };
            //        orderRows.Add(neworderrow);
            //    }

            //}
            //_ordersService.CreatOrderRows(orderRows);



        }
       

    }
}
