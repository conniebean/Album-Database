using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExamReview.Models
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> CarParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer()
                {
                    ManufacturerID = 1,
                    Name = "Ford",
                    Address = "1 American Road",
                    City = "Dearborn",
                    PostalCode = "12345",
                    Website = "https://www.ford.com/"
                },
                new Manufacturer()
                {
                    ManufacturerID = 2,
                    Name = "Toyota",
                    Address = "1510 Polk St",
                    City = "Houston",
                    PostalCode = "67891",
                    Website = "https://www.toyota.com/"
                }
                );

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    CarID = 1,
                    ManufacturerID = 1,
                    Make ="Ford",
                    Model = "Fusion",
                    Year = 2010
                },
                new Car()
                {
                    CarID =2,
                    ManufacturerID = 2,
                    Make = "Toyota",
                    Model = "Corolla",
                    Year = 2019
                }
                );
            modelBuilder.Entity<Part>().HasData(
                new Part()
                {
                    PartID = 1,
                    PartName = "Tires",
                    Price = 180.10,
                    CarID = 2
                },
                new Part()
                {
                    PartID = 2,
                    PartName = "Windshield Wipers",
                    Price = 24.99,
                    CarID = 1
                },
                new Part()
                {
                    PartID = 3,
                    PartName = "Condenser",
                    Price = 250.50,
                    CarID = 2
                },
                new Part()
                {
                    PartID = 4,
                    PartName = "Radiator",
                    Price = 190.99,
                    CarID = 1
                }
                );
        }
    }
}
