﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ETL.Models
{
    public partial class StageContext : DbContext
    {
        public StageContext()
        {
        }

        public StageContext(DbContextOptions<StageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airlines> Airlines { get; set; }
        public virtual DbSet<Airports> Airports { get; set; }
        public virtual DbSet<Flights> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airlines>(entity =>
            {
                entity.HasKey(e => e.AirlineId)
                    .HasName("PK__Airlines__26BE52368A1A77FA");

                entity.Property(e => e.AirlineId).HasColumnName("Airline_Id");

                entity.Property(e => e.Airline).HasMaxLength(70);

                entity.Property(e => e.IataCode)
                    .HasMaxLength(5)
                    .HasColumnName("IATA_Code");
            });

            modelBuilder.Entity<Airports>(entity =>
            {
                entity.HasKey(e => e.AirportId)
                    .HasName("PK__Airports__3DBD86F3AC36CBCE");

                entity.Property(e => e.AirportId).HasColumnName("Airport_Id");

                entity.Property(e => e.Airport).HasMaxLength(150);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Iata)
                    .HasMaxLength(5)
                    .HasColumnName("IATA");

                entity.Property(e => e.Latitude).HasMaxLength(10);

                entity.Property(e => e.Longitude).HasMaxLength(10);

                entity.Property(e => e.State).HasMaxLength(5);
            });

            modelBuilder.Entity<Flights>(entity =>
            {
                entity.HasKey(e => e.FlightId)
                    .HasName("PK__Flights__3DBD86F37A3BD583");

                entity.Property(e => e.FlightId).HasColumnName("Flight_Id");

                entity.Property(e => e.AirTime).HasColumnName("Air_Time");

                entity.Property(e => e.Airline).HasMaxLength(150);

                entity.Property(e => e.ArivalDelay).HasColumnName("Arival_Delay");

                entity.Property(e => e.ArivalTime)
                    .HasMaxLength(5)
                    .HasColumnName("Arival_Time");

                entity.Property(e => e.DayOfWeek).HasColumnName("Day_of_Week");

                entity.Property(e => e.DepartureDelay).HasColumnName("Departure_Delay");

                entity.Property(e => e.DepartureTime)
                    .HasMaxLength(5)
                    .HasColumnName("Departure_Time");

                entity.Property(e => e.DestinationAirport)
                    .HasMaxLength(5)
                    .HasColumnName("Destination_Airport");

                entity.Property(e => e.FlightNumber).HasColumnName("Flight_Number");

                entity.Property(e => e.OriginAirport)
                    .HasMaxLength(5)
                    .HasColumnName("Origin_Airport");

                entity.Property(e => e.ScheduledArival)
                    .HasMaxLength(5)
                    .HasColumnName("Scheduled_Arival");

                entity.Property(e => e.ScheduledDeparture)
                    .HasMaxLength(5)
                    .HasColumnName("Scheduled_Departure");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVERVK;Database=Stage;User Id=sa; Password=sa; TrustServerCertificate=True");
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}