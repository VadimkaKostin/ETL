using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ETL.Models;
using ETL.DataWarehouse;
using Microsoft.EntityFrameworkCore;

namespace ETL
{
    public static class Program
    {
        //Адреси джерел даних
        const string Airlines_FilePath = "D:\\Projects\\ADIS\\Lab1\\Airports\\airlines.csv";
        const string Airports_FilePath = "D:\\Projects\\ADIS\\Lab1\\Airports\\airports.csv";
        const string Flights_FilePath = "D:\\Projects\\ADIS\\Lab1\\Airports\\flights.csv";
        public static void Main(string[] args)
        {
            ETL etl = new ETL(Airlines_FilePath, Airports_FilePath, Flights_FilePath);

            //Вилучення даних у Stage зону
            etl.Extract();

            //Трансформація даних
            etl.Transform();

            //Завантаження даних у сзовище даних
            etl.Load();
        }
    }
}