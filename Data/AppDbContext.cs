
using DEMO1.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media;

namespace DEMO1.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=INBOOK_X3_PLUS;Initial Catalog=ShoeStoreDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;",
                options => options.EnableRetryOnFailure());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Login = "admin",
                Password = "123",
                FullName = "Главный админ",
                Role = "Admin"
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Login = "client",
                Password = "123",
                FullName = "Иванов Иван",
                Role = "Client"
            });
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Article = "A112T4",
                    Name = "Ботинки женские",
                    Price = 4990,
                    Category = "Женская обувь",
                    Quantity = 6,
                    ImagePath = "1.jpg"
                });
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 2,
                    Article = "F635R4",
                    Name = "Ботинки Marco",
                    Price = 4990,
                    Category = "Женская обувь",
                    Quantity = 13,
                    ImagePath = "2.jpg"
                });
            }
        } 
    }
