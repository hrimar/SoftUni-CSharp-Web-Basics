namespace SimpleMvc.Data
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    public class KittenDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Breed> Breeds { get; set; }

        public DbSet<Kitten> Kittens { get; set; }

      
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer(@"Server=DESKTOP-LPPTMS9\SQLEXPRESS;Database=KittenDb;Integrated Security=True;");

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
        }
    }
}
