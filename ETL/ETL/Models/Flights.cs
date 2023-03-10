﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ETL.Models
{
    public partial class Flights
    {
        public int FlightId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? DayOfWeek { get; set; }
        public string Airline { get; set; }
        public int? FlightNumber { get; set; }
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public string ScheduledDeparture { get; set; }
        public string DepartureTime { get; set; }
        public int? DepartureDelay { get; set; }
        public string ScheduledArival { get; set; }
        public string ArivalTime { get; set; }
        public int? ArivalDelay { get; set; }
        public int? AirTime { get; set; }
        public int? Distance { get; set; }
        public bool? Canceled { get; set; }
    }
}