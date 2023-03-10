using ETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL
{
    public partial class ETL
    {
        //Метод для трансформації даних в Stage зоні
        public void Transform()
        {
            TransformFlights();
        }
        //Метод для трансформації даних о рейсах в Stage зоні
        public void TransformFlights()
        {
            FlightTransformer flightTransformer = new FlightTransformer();

            using(StageContext db = new StageContext())
            {
                foreach(var flight in db.Flights)
                {
                    flightTransformer.Transform(flight);
                }
                db.SaveChanges();
            }
        }
    }
}
