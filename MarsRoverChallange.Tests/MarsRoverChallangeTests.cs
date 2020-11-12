using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoverChallange;
using System.IO;
using Xunit;

namespace MarsRoverChallange.Tests
{
    public class MarsRoverChallangeTests
    {
        [Fact]
        public void Move_Rovers()
        {

            //Rover move tests
            var logger = new FileLogger(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "MainLog");

            var plateau = new Plateau(logger);
            plateau.SetPlateauSize("5 5");


            //Add rover 1
            var rover1 = new Rover(logger, "rover1");

            plateau.AddRover(rover1, "1 2 N");

            plateau.MoveRover("rover1", "LMLMLMLMM");
            
            Assert.Equal("1 3 N", plateau.GetRoverPoition("rover1"));


            //Add rover 2
            var rover2 = new Rover(logger, "rover2");

            plateau.AddRover(rover2, "3 3 E");

            plateau.MoveRover("rover2", "MMRMMRMRRM");

            plateau.GetRoverPoition("rover2");

            Assert.Equal("5 1 E", plateau.GetRoverPoition("rover2"));

        }
        [Fact]
        public void Check_move_position()
        {
            //Test that rovers can't occupy the same space when moving
            var logger = new FileLogger(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "MainLog");

            //Plateau
            var plateau = new Plateau(logger);
            plateau.SetPlateauSize("5 5");


            //Add rover 1
            var rover1 = new Rover(logger, "rover1");

            plateau.AddRover(rover1, "1 2 N");

            plateau.MoveRover("rover1", "LMLMLMLMM");
            
            Assert.Equal("1 3 N", plateau.GetRoverPoition("rover1"));


            //Add rover 2
            var rover2 = new Rover(logger, "rover2");

            plateau.AddRover(rover2, "1 2 N");
                                             
            Assert.ThrowsAny< Exception > (() => plateau.MoveRover("rover2", "LMLMLMLMMMLMLMLM"));
        }

        [Fact]
        public void Check_set_position()
        {
            //Test that rovers can't occupy the same space when adding
            var logger = new FileLogger(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "MainLog");

            var plateau = new Plateau(logger);
            plateau.SetPlateauSize("5 5");

            var rover3 = new Rover(logger, "rover3");
                   
            Assert.ThrowsAny<Exception>(() => plateau.AddRover(rover3, "1 8 N"));
        }

    }
}
