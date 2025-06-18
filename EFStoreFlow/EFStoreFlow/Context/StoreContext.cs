using EFStoreFlow.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFStoreFlow.Context
{
    public class StoreContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-33VDDOP\\SQLEXPRESS17;initial Catalog=StoreFlowDb;integrated Security=true; trust server certificate=true;");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
    }
}
