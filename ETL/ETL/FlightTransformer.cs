using ETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL
{
    //Класс для трансформації даних о рейсі
    public class FlightTransformer
    {
        //Методя для змінення формату часу
        private string TransformTime(string time)
        {
            if (time.Length > 4)
                throw new ArgumentException("Incorrect format of time");

            return time.Substring(0,2) + ":" + time.Substring(2);
        }
        //Метод для валідації дати
        private void ValidateDate(Flights flights)
        {
            if(flights.ScheduledDeparture == "24:00")
            {
                DateTime date = Convert.ToDateTime($"{flights.Year}-{flights.Month}-{flights.Day}");
                date = date.AddDays(1);

                flights.Year = date.Year;
                flights.Month = date.Month;
                flights.Day = date.Day;
            }    
        }
        //Метод для валідації часу
        private string ValidateTime(string time)
        {
            if (time == "24:00")
                time = "00:00";
            return time;
        }
        //Метод для тарнсформації даних о рейсі
        public void Transform(Flights flight)
        {
            //Змінення формату часу
            flight.ScheduledDeparture = this.TransformTime(flight.ScheduledDeparture);
            flight.DepartureTime = this.TransformTime(flight.DepartureTime);
            flight.ScheduledArival = this.TransformTime(flight.ScheduledArival);
            flight.ArivalTime = this.TransformTime(flight.ArivalTime);

            //Валідація дати
            ValidateDate(flight);

            //Валідація часу
            flight.ScheduledDeparture = this.ValidateTime(flight.ScheduledDeparture);
            flight.DepartureTime = this.ValidateTime(flight.DepartureTime);
            flight.ScheduledArival = this.ValidateTime(flight.ScheduledArival);
            flight.ArivalTime = this.ValidateTime(flight.ArivalTime);
        }
    }
}
