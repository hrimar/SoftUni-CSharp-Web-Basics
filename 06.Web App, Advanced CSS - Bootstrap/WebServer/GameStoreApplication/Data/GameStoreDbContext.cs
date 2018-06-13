using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebServer.GameStoreApplication.Data.Models;

namespace WebServer.GameStoreApplication.Data
{
    public class GameStoreDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer(@"Server=.;Database=GameStoreDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.Games)
                .WithOne(ug => ug.User)
                .HasForeignKey(ug => ug.UserId);

            builder.Entity<Game>()
               .HasMany(u => u.Users)
               .WithOne(ug => ug.Game)
                 .HasForeignKey(ug => ug.GameId);

            builder.Entity<UserGame>()
                .HasKey(u => new { u.UserId, u.GameId });


            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

        }
    }
}
