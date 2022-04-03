using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Context
{
    public class CityInfoContext:DbContext
    {
        public DbSet<city> Cities { get; set; }

        public DbSet<PointOfInterest> PointOfInterests { get; set; }
        public CityInfoContext(DbContextOptions<CityInfoContext> option):base(option)
        {
           // Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
