using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETL.Models;
using ETL.DataWarehouse;

namespace ETL
{
    public partial class ETL
    {
        //Метод для завантаження даних із Stage зони у сховище даних
        public void Load()
        {
            LoadStates();
            LoadCities();
            LoadAirports();
            LoadAirlines();
            LoadDate();
            LoadFlightNumber();
            LoadFacts();
        }
        //Метод для завантаження таблиці виміру Dim_State
        public void LoadStates()
        {
            using(StageContext stage = new StageContext())
            {
                using(ApplicationContext dw = new ApplicationContext())
                {
                    foreach(var airport in stage.Airports)
                    {
                        if (dw.DimState.Any(state => state.Name == airport.State))
                            continue;
                        else
                        {
                            DimState state = new DimState()
                            {
                                Name = airport.State,
                                Country = airport.Country
                            };
                            dw.DimState.Add(state);
                            dw.SaveChanges();
                        }
                    }
                }
            }
        }
        //Метод для завантаження таблиці виміру Dim_City
        public void LoadCities()
        {
            using (StageContext stage = new StageContext())
            {
                using (ApplicationContext dw = new ApplicationContext())
                {
                    foreach (var airport in stage.Airports)
                    {
                        if (dw.DimCity.Any(city => city.Name == airport.City))
                            continue;
                        else
                        {
                            int stateId = dw.DimState.First(state => state.Name == airport.State).StateId;
                            DimCity city = new DimCity()
                            {
                                Name = airport.City,
                                StateId = stateId
                            };
                            dw.DimCity.Add(city);
                            dw.SaveChanges();
                        }
                    }
                }
            }
        }
        //Метод для завантаження таблиці виміру Dim_Airports
        public void LoadAirports()
        {
            using(StageContext stage = new StageContext())
            {
                using(ApplicationContext dw = new ApplicationContext())
                {
                    foreach(var airport in stage.Airports)
                    {
                        int cityId = dw.DimCity.First(city => city.Name == airport.City).CityId;

                        DimAirports dimAirports = new DimAirports()
                        {
                            AirportId = airport.AirportId,
                            Name = airport.Airport,
                            CityId = cityId,
                            Iata = airport.Iata,
                            Latitude = airport.Latitude,
                            Longitude = airport.Longitude
                        };
                        dw.DimAirports.Add(dimAirports);
                        dw.SaveChanges();
                    }
                }
            }
        }
        //Метод для завантаження таблиці виміру Dim_Airlines
        public void LoadAirlines()
        {
            using (StageContext stage = new StageContext())
            {
                using (ApplicationContext dw = new ApplicationContext())
                {
                    foreach (var airline in stage.Airlines)
                    {
                        DimAirlines dimAirlines = new DimAirlines()
                        {
                            AirlineId = airline.AirlineId,
                            Name = airline.Airline,
                            Iata = airline.IataCode
                        };
                        dw.DimAirlines.Add(dimAirlines);
                        dw.SaveChanges();
                    }
                }
            }
        }
        //Метод для завантаження таблиці виміру Dim_Date
        public void LoadDate()
        {
            using (StageContext stage = new StageContext())
            {
                using (ApplicationContext dw = new ApplicationContext())
                {
                    foreach(var flight in stage.Flights)
                    {
                        if (dw.DimDate.Any(date => date.Year == flight.Year && 
                        date.Month == flight.Month && date.Day == flight.Day))
                            continue;
                        else
                        {
                            DimDate dimDate = new DimDate()
                            {
                                Year = flight.Year,
                                Month = flight.Month,
                                Day = flight.Day,
                                DayOfWeek = flight.DayOfWeek,
                            };
                            dw.DimDate.Add(dimDate);
                            dw.SaveChanges();
                        }
                    }
                }
            }
        }
        //Метод для завантаження таблиці виміру Dim_Flight_Number
        public void LoadFlightNumber()
        {
            using(StageContext stage = new StageContext())
            {
                using (ApplicationContext dw = new ApplicationContext())
                {
                    foreach(var flight in stage.Flights)
                    {
                        if (dw.DimFlightNumber.Any(number => number.FlightNumber == flight.FlightNumber))
                            continue;
                        else
                        {
                            DimFlightNumber dimFlightNumber = new DimFlightNumber()
                            {
                                FlightNumber = flight.FlightNumber
                            };
                            dw.DimFlightNumber.Add(dimFlightNumber);
                            dw.SaveChanges();
                        }
                    }
                }
            }
        }
        //Метод для завантаження таблиць факту Fact_Flights і Fact_Delays
        public void LoadFacts()
        {
            using(StageContext stage = new StageContext())
            {
                using (ApplicationContext dw = new ApplicationContext())
                {
                    foreach(var flight in stage.Flights)
                    {
                        int dateId = dw.DimDate.First(date => date.Year == flight.Year &&
                        date.Month == flight.Month && date.Day == flight.Day).DateId;

                        int flightNumberId = dw.DimFlightNumber
                            .First(number => number.FlightNumber == flight.FlightNumber).FlightNumberId;

                        int airlineId = dw.DimAirlines.First(airline => airline.Iata == flight.Airline).AirlineId;

                        int originAirportId = dw.DimAirports.First(airport => airport.Iata == flight.OriginAirport).AirportId;
                        int destinationAirportId = dw.DimAirports.First(airport => airport.Iata == flight.DestinationAirport).AirportId;

                        FactFlights factFlights = new FactFlights()
                        {
                            FactFlightsId = flight.FlightId,
                            DateId = dateId,
                            FlightNumberId = flightNumberId,
                            AirlineId = airlineId,
                            OriginAirportId = originAirportId,
                            DestinationAirportId = destinationAirportId,
                            AirTime = flight.AirTime,
                            Distance = flight.Distance,
                            Canceled = flight.Canceled
                        };

                        FactDelays factDelays = new FactDelays()
                        {
                            FactFlightsId = flight.FlightId,
                            ScheduledDeparture = TimeOnly.Parse(flight.ScheduledDeparture),
                            DepartureTime = TimeOnly.Parse(flight.DepartureTime),
                            DepartureDelay = flight.DepartureDelay,
                            ScheduledArival = TimeOnly.Parse(flight.ScheduledArival),
                            ArivalTime = TimeOnly.Parse(flight.ArivalTime),
                            ArivalDelay = flight.ArivalDelay
                        };

                        dw.FactFlights.Add(factFlights);
                        dw.FactDelays.Add(factDelays);
                    }
                    dw.SaveChanges();
                }
            }
        }
    }
}
