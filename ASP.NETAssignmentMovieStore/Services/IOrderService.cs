using ASP.NETAssignmentMovieStore.Models;

namespace ASP.NETAssignmentMovieStore.Services
{
    public interface IOrderService
    {
        List<Orders> GetOrders(int CustomerId);

        Orders GetOrder(int id);
        public void CreatOrder(Orders o);

        public void CreatOrderRows(List<OrderRows> orderRows);

        public void CreateOrdersAndOrderRows(Cart cartitems, Customers custid);
    }
}
