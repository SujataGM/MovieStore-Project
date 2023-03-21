using ASP.NETAssignmentMovieStore.Data;
using ASP.NETAssignmentMovieStore.Models;
using ASP.NETAssignmentMovieStore.Services;
using Microsoft.AspNetCore.Mvc;
using ASP.NETAssignmentMovieStore.Models.ViewModels;

namespace ASP.NETAssignmentMovieStore.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _db;
        private int id;

        public CustomerService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Customers> GetCustomers()
        {
            List<Customers> customers = _db.Customers.ToList();

            return customers;
        }
        public Customers GetCustomer(int Id)
        {
            return _db.Customers.Find(Id);
        }
       

        public void EditCustomer(Customers customers)
        {
            _db.Customers.Update(customers);
            _db.SaveChanges();
        }
        public void DeleteCustomer(int id)
        {
            var deleterecord = _db.Customers.Find(id);
            if (id != null)
            {
                _db.Customers.Remove(deleterecord);
                _db.SaveChanges();
            }
        }
        public void CreateCustomer([Bind("FirstName,LastName,BillingAddress,BillingZip,Billing City,Delivery Address,Delivery Zip,Delivery City,Email Address,Phone No")] Customers customer)
        {
            
                _db.Customers.Add(customer);
                _db.SaveChanges();
            
        }
            public Customers DetailsCustomer(int id)
            {
            if (id != null)
            {

                var customer = _db.Customers.FirstOrDefault(c => c.Id == id);

                return customer;

            }
            else
                return null;


        }

       


    }  
}
