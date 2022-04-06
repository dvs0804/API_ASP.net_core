﻿// <auto-generated />
using CityInfo.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CityInfo.API.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    partial class CityInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointOfInterest");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Name = "central park",
                            description = "the most visited urban park in the united states"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            Name = "Empire State Building",
                            description = "A 102-story skyscraper located in Midtown Manhathan"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 2,
                            Name = "Cathedral",
                            description = "A Gothic style cathedral,conceived by archited jan"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 2,
                            Name = "Antwerp Central Station",
                            description = "the finest example of railway architeture in belgium"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 3,
                            Name = "Eiffel Tower",
                            description = " a wrought iron lattice tower on the champ de mars name"
                        },
                        new
                        {
                            Id = 6,
                            CityId = 3,
                            Name = "the louvre",
                            description = "the worlds largest museum"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.city", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("descripcion")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "New York",
                            descripcion = "the one with that big park"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Antwerp",
                            descripcion = "the one with the catedral that was never really"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Paris",
                            descripcion = "the one with that big tower"
                        });
                });

            modelBuilder.Entity("CityInfo.API.Entities.PointOfInterest", b =>
                {
                    b.HasOne("CityInfo.API.Entities.city", "city")
                        .WithMany("PointOfInterest")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");
                });

            modelBuilder.Entity("CityInfo.API.Entities.city", b =>
                {
                    b.Navigation("PointOfInterest");
                });
#pragma warning restore 612, 618
        }
    }
}
