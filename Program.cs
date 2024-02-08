using System;
using System.Data;
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using HelloWorld.Data;

namespace HelloWorld // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();
            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

            Computer myComputer = new Computer()
            {
                Motherboard = "RIJND1111",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 999.67m,
                VideoCard = "GTX1040"
            };

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + myComputer.Motherboard 
                        +  "' , '" + myComputer.HasWifi
                         +  "' , '" + myComputer.HasLTE
                          +  "' , '" + myComputer.ReleaseDate
                           +  "' , '" + myComputer.Price
                            +  "' , '" + myComputer.VideoCard
            + "')";
            
            bool result = dapper.ExecuteSql(sql);


            string sqlSelect = @"SELECT 
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard   
                FROM TutorialAppSchema.Computer";
            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            foreach(Computer singleComputer in computers)
            {
                Console.WriteLine("'" + myComputer.Motherboard 
                        +  "' , '" + myComputer.HasWifi
                         +  "' , '" + myComputer.HasLTE
                          +  "' , '" + myComputer.ReleaseDate
                           +  "' , '" + myComputer.Price
                            +  "' , '" + myComputer.VideoCard
            + "'");
            }
        }
    }
}