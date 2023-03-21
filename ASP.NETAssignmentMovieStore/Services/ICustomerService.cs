using ASP.NETAssignmentMovieStore.Models;

namespace ASP.NETAssignmentMovieStore.Services
{
    public interface ICustomerService
    {
        List<Customers> GetCustomers();

        Customers GetCustomer(int id);

        void EditCustomer(Customers customer);
        void DeleteCustomer(int id);
        void CreateCustomer(Customers customer);
        Customers DetailsCustomer(int id);



     }
}
