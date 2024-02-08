using HelloWorld.Models;
using HelloWorld.Data;

namespace HelloWorld 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();
            DataContextEF entityFramework = new DataContextEF();


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

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

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
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard   
                FROM TutorialAppSchema.Computer";
            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
            Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate'" + ",'Price','VideoCard'");
            foreach(Computer singleComputer in computers)
            {
                Console.WriteLine("'" + singleComputer.ComputerId 
                        +  "' , '" + singleComputer.Motherboard
                        +  "' , '" + singleComputer.HasWifi
                        +  "' , '" + singleComputer.HasLTE
                        +  "' , '" + singleComputer.ReleaseDate
                        +  "' , '" + singleComputer.Price
                        +  "' , '" + singleComputer.VideoCard
            + "'");
            }


            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();
            if(computersEF != null)
            {
                 Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate'" 
                    + ",'Price','VideoCard'");
                foreach(Computer singleComputer in computersEF)
                {
                 Console.WriteLine("'" + singleComputer.ComputerId 
                        +  "' , '" + singleComputer.Motherboard
                        +  "' , '" + singleComputer.HasWifi
                        +  "' , '" + singleComputer.HasLTE
                        +  "' , '" + singleComputer.ReleaseDate
                        +  "' , '" + singleComputer.Price
                        +  "' , '" + singleComputer.VideoCard
            + "'");
                }
            }

        }
    }
}