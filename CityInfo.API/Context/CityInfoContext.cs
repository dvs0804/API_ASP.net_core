using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Context
{
    public class CityInfoContext:DbContext
    {
        public DbSet<city> Cities { get; set; }

        public DbSet<PointOfInterest> PointOfInterest { get; set; }
        public CityInfoContext(DbContextOptions<CityInfoContext> option):base(option)
        {
           // Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<city>()
                .HasData(new city()
                {
                    Id = 1,
                    Name ="New York",
                    descripcion = "the one with that big park"
                },
                new city()
                {
                    Id=2,
                    Name ="Antwerp",
                    descripcion = "the one with the catedral that was never really"
                },
                new city()
                { 
                    Id =3,
                    Name ="Paris",
                    descripcion = "the one with that big tower"
                });
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                 new PointOfInterest()
                 {
                     Id = 1,
                     CityId = 1,
                     Name = "central park",
                     description = "the most visited urban park in the united states"
                     
                 },
                 new PointOfInterest() 
                 {
                    Id =2,
                    CityId =1,
                    Name ="Empire State Building",
                    description ="A 102-story skyscraper located in Midtown Manhathan"
                 },
                 new PointOfInterest()
                 {
                     Id =3, 
                     CityId =2,
                     Name ="Cathedral",
                     description = "A Gothic style cathedral,conceived by archited jan"
                 },
                 new PointOfInterest()
                 {
                     Id = 4,
                     CityId=2,
                     Name ="Antwerp Central Station",
                     description = "the finest example of railway architeture in belgium"
                 },
                 new PointOfInterest()
                 {
                     Id=5,
                     CityId=3,
                     Name = "Eiffel Tower",
                     description = " a wrought iron lattice tower on the champ de mars name"
                 },
                 new PointOfInterest() 
                 {
                     Id=6,
                     CityId=3,
                     Name = "the louvre",
                     description = "the worlds largest museum"
                 }
                );
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
