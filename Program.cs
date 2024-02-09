﻿using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using AutoMapper.Internal.Mappers;

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

            string computersJSON = File.ReadAllText("ComputersSnake.json");
            Mapper mapper = new Mapper(new MapperConfiguration((cfg) => {
                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(destination => destination.ComputerId, options=>
                        options.MapFrom(source=>source.computer_id))
                    .ForMember(destination => destination.CPUCores, options=>
                        options.MapFrom(source=>source.cpu_cores))
                    .ForMember(destination => destination.HasWifi, options=>
                        options.MapFrom(source=>source.has_wifi))
                    .ForMember(destination => destination.HasLTE, options=>
                        options.MapFrom(source=>source.has_lte))
                    .ForMember(destination => destination.Price, options=>
                        options.MapFrom(source=>source.price))
                    .ForMember(destination => destination.Motherboard, options=>
                        options.MapFrom(source=>source.motherboard))
                    .ForMember(destination => destination.VideoCard, options=>
                        options.MapFrom(source=>source.video_card))
                    .ForMember(destination => destination.ReleaseDate, options=>
                        options.MapFrom(source=>source.release_date));
            }));

            IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJSON);
            if(computersSystem != null)
            {
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);
                foreach(Computer computer in computerResult)
                {
                    Console.WriteLine(computer.Motherboard);
                }
            }
        //     IEnumerable<Computer>? computersNewtonsoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJSON); //newtonsoft

        //     if(computersNewtonsoft != null)
        //     {
        //         foreach(Computer computer in computersNewtonsoft)
        //         {
        //             string sql = @"INSERT INTO TutorialAppSchema.Computer (
        //                             Motherboard,
        //                             HasWifi,
        //                             HasLTE,
        //                             ReleaseDate,
        //                             Price,
        //                             VideoCard
        //                         ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
        //                                     +  "' , '" + computer.HasWifi
        //                                     +  "' , '" + computer.HasLTE
        //                                     +  "' , '" + computer.ReleaseDate
        //                                     +  "' , '" + computer.Price
        //                                     +  "' , '" + EscapeSingleQuote(computer.VideoCard)
        //                         + "')";

        //                         dapper.ExecuteSql(sql);
        //         }
        //     }

        //     JsonSerializerSettings settings = new JsonSerializerSettings(){
        //         ContractResolver = new CamelCasePropertyNamesContractResolver()
        //     };
        //     string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings);//newtonsoft
        //     File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

        //     string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersNewtonsoft, options);//System.Text.Json
        //     File.WriteAllText("computersCopySystem.txt", computersCopySystem);

        //     static string EscapeSingleQuote(string input)
        //     {
        //         return input.Replace("'", "''");
        //     }

        }
    }
}