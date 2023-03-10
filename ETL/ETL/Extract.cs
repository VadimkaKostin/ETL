using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETL.Models;

namespace ETL
{
    public partial class ETL
    {
        //Шлях до джерел даних
        private readonly string _airlines_FilePath;
        private readonly string _airports_FilePath;
        private readonly string _flights_FilePath;

        public ETL(string airlines_FilePath, string airports_FilePath, string fliagths_FilePath)
        {
            this._airlines_FilePath = airlines_FilePath;
            this._airports_FilePath = airports_FilePath;
            this._flights_FilePath = fliagths_FilePath;
        }
        //Метод для вилучення даних і завантаження Stage зони
        public void Extract()
        {
            ExtractAirlines();
            ExtractAirports();
            ExtractFlights();
        }
        //Метод для завантаження даних про авіакомпанії в Stage зону
        private void ExtractAirlines()
        {
            List<Airlines> airlines = File.ReadAllLines(this._airlines_FilePath)
                .Skip(1)
                .Select(csvString => CsvParse.ToAirlines(csvString))
                .ToList();

            using(StageContext db = new StageContext())
            {
                foreach(Airlines airline in airlines)
                {
                    db.Airlines.Add(airline);
                }
                db.SaveChanges();
            }
        }
        //Метод для завантаження даних про аеропорти в Stage зону
        private void ExtractAirports()
        {
            List<Airports> airports = File.ReadAllLines(this._airports_FilePath)
                .Skip(1)
                .Select(csvString => CsvParse.ToAirports(csvString))
                .ToList();

            using(StageContext db = new StageContext())
            {
                foreach(Airports airpot in airports)
                {
                    db.Airports.Add(airpot);
                }
                db.SaveChanges();
            }
        }
        //Метод для завантаження даних про рейси в Stage зону
        private void ExtractFlights()
        {
            List<Flights> flights = File.ReadAllLines(this._flights_FilePath)
                .Skip(1)
                .Select(csvString => CsvParse.ToFlights(csvString))
                .ToList();

            using(StageContext db = new StageContext())
            {
                foreach(Flights flight in flights)
                {
                    if (flight == null)
                        continue;

                    if (flight.Month > 1)
                        break;

                    db.Flights.Add(flight);
                }
                db.SaveChanges();
            }
        }
    }
}
