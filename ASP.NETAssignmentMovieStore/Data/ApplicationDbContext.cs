using ASP.NETAssignmentMovieStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASP.NETAssignmentMovieStore.Models;
using ASP.NETAssignmentMovieStore.Models.ViewModels;

namespace ASP.NETAssignmentMovieStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<OrderRows> OrderRows { get; set; }
        public DbSet<Orders> Orders { get; set; }
       
        


    }
}