﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ETL.DataWarehouse
{
    public partial class DimFlightNumber
    {
        public DimFlightNumber()
        {
            FactFlights = new HashSet<FactFlights>();
        }

        public int FlightNumberId { get; set; }
        public int? FlightNumber { get; set; }

        public virtual ICollection<FactFlights> FactFlights { get; set; }
    }
}