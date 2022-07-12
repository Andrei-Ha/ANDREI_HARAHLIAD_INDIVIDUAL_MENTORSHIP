﻿// <auto-generated />
using System;
using Exadel.Forecast.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Exadel.Forecast.Api.Migrations
{
    [DbContext(typeof(WeatherDbContext))]
    partial class WeatherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Exadel.Forecast.Domain.Models.CurrentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ForecastModelId")
                        .HasColumnType("int");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ForecastModelId");

                    b.ToTable("CurrentModel");
                });

            modelBuilder.Entity("Exadel.Forecast.Domain.Models.DayModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("AvgTemperature")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ForecastModelId")
                        .HasColumnType("int");

                    b.Property<double>("MaxTemperature")
                        .HasColumnType("float");

                    b.Property<double>("MinTemperature")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ForecastModelId");

                    b.ToTable("DayModel");
                });

            modelBuilder.Entity("Exadel.Forecast.Domain.Models.ForecastModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CurrentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrentId");

                    b.ToTable("ForecastModels");
                });

            modelBuilder.Entity("Exadel.Forecast.Domain.Models.CurrentModel", b =>
                {
                    b.HasOne("Exadel.Forecast.Domain.Models.ForecastModel", null)
                        .WithMany("History")
                        .HasForeignKey("ForecastModelId");
                });

            modelBuilder.Entity("Exadel.Forecast.Domain.Models.DayModel", b =>
                {
                    b.HasOne("Exadel.Forecast.Domain.Models.ForecastModel", null)
                        .WithMany("Days")
                        .HasForeignKey("ForecastModelId");
                });

            modelBuilder.Entity("Exadel.Forecast.Domain.Models.ForecastModel", b =>
                {
                    b.HasOne("Exadel.Forecast.Domain.Models.CurrentModel", "Current")
                        .WithMany()
                        .HasForeignKey("CurrentId");

                    b.Navigation("Current");
                });

            modelBuilder.Entity("Exadel.Forecast.Domain.Models.ForecastModel", b =>
                {
                    b.Navigation("Days");

                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}
