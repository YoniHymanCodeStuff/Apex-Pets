using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data.DataAccess
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals {get;set;}
        public DbSet<Customer> Customers {get;set;}
        public DbSet<Admin> Admins {get;set;}
        public DbSet<User> Users {get;set;} 

        //this part is just to rename i think. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Animal>().ToTable("Animal");
        }
    }
}