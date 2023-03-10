using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETL.DataWarehouse;

namespace ETL
{
    //Клас для обнови даних у сховищі даних
    public class DataWarehouseUpdater
    {
        //Вбудований клас моделі для роботи з CSV файлом
        public class CityNamesLine
        {
            public string? NewName { get; set; }
            public string? CurrentName { get; set; }
            public string? OldName { get; set; }

            public static CityNamesLine Parse(string lineCSV)
            {
                string[] arguments = lineCSV.Split(new char[] { ',' });

                CityNamesLine cityNamesLine = new CityNamesLine()
                {
                    NewName = arguments[0],
                    CurrentName = arguments[1],
                    OldName = arguments[2]
                };
                
                return cityNamesLine;
            }

            public string ToCSV()
            {
                return string.Join(',', NewName, CurrentName, OldName);
            }
        }

        //Метод для обнови даних у сховищі даних
        public void Update()
        {
            UpdateCities();
        }
        //Метод для обнови виміру Dim_City
        public void UpdateCities()
        {
            const string path = "D:\\Projects\\ADIS\\Lab1\\ETL\\ETL\\FilesCSV\\CityNames.csv";

            const string header = "New_Name,Current_Name,Old_Name";

            List<CityNamesLine> cityNamesLines = File.ReadAllLines(path)
                .Skip(1).Select(str => CityNamesLine.Parse(str)).ToList();

            using (ApplicationContext dw = new ApplicationContext())
            {
                foreach (var cityNames in cityNamesLines)
                {
                    if (string.IsNullOrEmpty(cityNames.NewName))
                        continue;

                    var city = dw.DimCity.FirstOrDefault(x => x.Name == cityNames.CurrentName);
                    if (city != null)
                        city.Name = cityNames.NewName;

                    cityNames.OldName = cityNames.CurrentName;
                    cityNames.CurrentName = cityNames.NewName;
                    cityNames.NewName = String.Empty;
                }
                dw.SaveChanges();
            }

            List<string> stringsCSV = cityNamesLines.Select(cityLine => cityLine.ToCSV()).ToList();

            stringsCSV.Insert(0, header);

            File.WriteAllLines(path, stringsCSV);
        }
    }
}
