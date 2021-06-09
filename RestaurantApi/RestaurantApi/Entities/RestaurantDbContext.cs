using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private string _connactionString = "Server=localhost\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        //Nadanie że coś jest wymagane
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();
        }

        //Jaki typ bazy danych i jak połączyć
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connactionString);
        }

    }
}
