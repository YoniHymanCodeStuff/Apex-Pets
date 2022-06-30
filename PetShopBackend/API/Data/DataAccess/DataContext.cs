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
        public DbSet<Photo> Photos {get;set;}

        public DbSet<Order> Orders {get;set;}
        public DbSet<DeliveryAdress> Adresses {get;set;}

        //this part is just to rename i think.// why am I doing this here? 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Animal>().ToTable("Animal");

            modelBuilder.Entity<User>()
            .HasDiscriminator(x => x.UserType);

            modelBuilder.Entity<User>()
            .Property(e => e.UserType)
            .HasMaxLength(20)
            .HasColumnName("User_type");
        }
    }
}