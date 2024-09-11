using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Context
{
    public class ApplicationDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-Q6Q8TVP;Database=TASK04;Trusted_Connection=True;Encrypt=false");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var categories = new List<Category>
        {
            new Category { CategoryId = 1, Name = "Electronics", Description = "Electronic Devices" },
            new Category { CategoryId = 2, Name = "Books", Description = "Various Books" },
            new Category { CategoryId = 3, Name = "Clothing", Description = "Men and Women Clothing" }
        };
            

            
            var products = new List<Product>
        {
            new Product { ProductId = 1, Title = "Smartphone", Price = 699.99m, Description = "Latest smartphone", Quantity = 50, ImagePath = "1.jpg", CategoryId = 1 },
            new Product { ProductId = 2, Title = "Laptop", Price = 999.99m, Description = "High performance laptop", Quantity = 20, ImagePath = "2.jpg", CategoryId = 1 },
            new Product { ProductId = 3, Title = "Novel", Price = 15.99m, Description = "Fiction novel", Quantity = 100, ImagePath = "3.jpg", CategoryId = 2 }
        };
            

            
            var users = new List<User>
        {
            new User { UserId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Password = "password123" },
            new User { UserId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", Password = "password456" }
        };
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Category>().HasData(categories);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

       
    }
 }
