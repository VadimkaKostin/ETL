﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ETL.DataWarehouse
{
    public partial class DimAirlines
    {
        public DimAirlines()
        {
            FactFlights = new HashSet<FactFlights>();
        }

        public int AirlineId { get; set; }
        public string Name { get; set; }
        public string Iata { get; set; }

        public virtual ICollection<FactFlights> FactFlights { get; set; }
    }
}