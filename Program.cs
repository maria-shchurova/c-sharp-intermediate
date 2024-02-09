using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;

namespace HelloWorld 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('" + myComputer.Motherboard 
            //             +  "' , '" + myComputer.HasWifi
            //              +  "' , '" + myComputer.HasLTE
            //               +  "' , '" + myComputer.ReleaseDate
            //                +  "' , '" + myComputer.Price
            //                 +  "' , '" + myComputer.VideoCard
            // + "')";
            
            // File.WriteAllText("log.txt", "\n" + sql + "\n");
            // using StreamWriter openFile = new("log.txt", append: true);
            // openFile.WriteLine("\n" + sql + "\n");
            // openFile.Close();

            string computersJSON = File.ReadAllText("Computers.json");
            //Console.WriteLine(computersJSON);
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            //IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJSON, options); //System.Text.Json
            IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJSON); //newtonsoft

            if(computers != null)
            {
                foreach(Computer computer in computers)
                {
                     Console.WriteLine(computer.Motherboard);
                }
            }
        }
    }
}