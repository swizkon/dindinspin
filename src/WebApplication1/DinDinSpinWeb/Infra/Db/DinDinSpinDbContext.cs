
using System;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infra.Db
{
    public class DinDinSpinDbContext : DbContext
    {
        public static string DbConnectionString = "Server=localhost; Database=DinDinSpinDb; Trusted_Connection=True;";

        public DinDinSpinDbContext(DbContextOptions<DinDinSpinDbContext> options)
            : base(options)
        { }

        public DbSet<Spinner> Spinners { get; set; }

        public DbSet<Dinner> Dinners { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // //GUESTS
            // modelBuilder.Entity<Guest>().HasData(new Guest("Alper Ebicoglu", DateTime.Now.AddDays(-10)) { Id = 1 });
            // modelBuilder.Entity<Guest>().HasData(new Guest("George Michael", DateTime.Now.AddDays(-5)) { Id = 2 });
            // modelBuilder.Entity<Guest>().HasData(new Guest("Daft Punk", DateTime.Now.AddDays(-1)) { Id = 3 });

            // //ROOMS
            // modelBuilder.Entity<Room>().HasData(new Room(101, "yellow-room", RoomStatus.Available, false) { Id = 1 });
            // modelBuilder.Entity<Room>().HasData(new Room(102, "blue-room", RoomStatus.Available, false) { Id = 2 });
            // modelBuilder.Entity<Room>().HasData(new Room(103, "white-room", RoomStatus.Unavailable, false) { Id = 3 });
            // modelBuilder.Entity<Room>().HasData(new Room(104, "black-room", RoomStatus.Unavailable, false) { Id = 4 });

            // //RESERVATIONS
            // modelBuilder.Entity<Reservation>().HasData(new Reservation(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(3), 3, 1) { Id = 1 });
            // modelBuilder.Entity<Reservation>().HasData(new Reservation(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(4), 4, 2) { Id = 2 });


            base.OnModelCreating(modelBuilder);
        }
    }
}