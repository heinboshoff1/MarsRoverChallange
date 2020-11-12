using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarsRoverChallange
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleLogger = new ConsoleLogger();

            try
            {

                var plateau = new Plateau(consoleLogger);
                plateau.SetPlateauSize("5 5");


                var rover1 = new Rover(consoleLogger, "rover1");

                plateau.AddRover(rover1, "1 2 N");

                plateau.MoveRover("rover1", "LMLMLMLMM");

                plateau.GetRoverPoition("rover1");



                var rover2 = new Rover(consoleLogger, "rover2");

                plateau.AddRover(rover2, "3 3 E");

                plateau.MoveRover("rover2", "MMRMMRMRRM");

                plateau.GetRoverPoition("rover2");


                var rover3 = new Rover(consoleLogger, "rover3");

                plateau.AddRover(rover3, "1 2 N");

                plateau.MoveRover("rover3", "LMLMLMLMMMLMLMLM");

                plateau.GetRoverPoition("rover3");





            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                consoleLogger.Log($"Error: {e.Message}");
            }

            Console.ReadKey();
        }
    }
  
}
