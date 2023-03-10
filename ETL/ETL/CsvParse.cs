using ETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL
{
    public static class CsvParse
    {
        public static Airlines? ToAirlines(string csvAirline)
        {
            try
            {
                string[] arguments = csvAirline.Split(new char[] { ',' });
                Airlines airlines = new Airlines()
                {
                    IataCode = arguments[0],
                    Airline = arguments[1]
                };
                return airlines;
            }
            catch(Exception)
            {
                return null;
            }
        }
        public static Airports? ToAirports(string csvAirport)
        {
            try
            {
                string[] arguments = csvAirport.Split(new char[] { ',' });
                Airports airports = new Airports()
                {
                    Iata = arguments[0],
                    Airport = arguments[1],
                    City = arguments[2],
                    State = arguments[3],
                    Country = arguments[4],
                    Latitude = arguments[5],
                    Longitude = arguments[6]
                };

                return airports;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Flights? ToFlights(string csvFlights)
        {
            try
            {
                string[] arguments = csvFlights.Split(new char[] { ',' });

                int? year = int.Parse(arguments[0]);
                int? month = int.Parse(arguments[1]);
                int? day = int.Parse(arguments[2]);
                int? dayOfWeek = int.Parse(arguments[3]);
                string airline = arguments[4];
                int? flightNumber = int.Parse(arguments[5]);
                string originAirport = arguments[7];
                string destinationAirport = arguments[8];
                string scheduledDeparture = arguments[9];
                string departureTime = arguments[10];
                int? departureDelay = int.Parse(arguments[11]);
                int? airTime = int.Parse(arguments[16]);
                int? distance = int.Parse(arguments[17]);
                string scheduledArival = arguments[20];
                string arivalTime = arguments[21];
                int? arivalDelay = int.Parse(arguments[22]);
                bool? canceled = int.Parse(arguments[24]) == 1 ? true : false;

                Flights flights = new Flights()
                {
                    Year = year,
                    Month = month,
                    Day = day,
                    DayOfWeek = dayOfWeek,
                    Airline = airline,
                    FlightNumber = flightNumber,
                    OriginAirport = originAirport,
                    DestinationAirport = destinationAirport,
                    ScheduledDeparture = scheduledDeparture,
                    DepartureTime = departureTime,
                    DepartureDelay = departureDelay,
                    AirTime = airTime,
                    Distance = distance,
                    ScheduledArival = scheduledArival,
                    ArivalTime = arivalTime,
                    ArivalDelay = arivalDelay,
                    Canceled = canceled
                };

                return flights;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
