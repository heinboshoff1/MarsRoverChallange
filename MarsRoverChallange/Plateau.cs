using System;
using System.Collections.Generic;
using System.Linq;


namespace MarsRoverChallange
{
    public class Plateau
    {
        public int _xSize { get; set; }
        public int _ySize { get; set; }

        private readonly ILogger _logger;

        public List<Rover> rovers = new List<Rover>();

        public Plateau(ILogger logger)
        {           
            _logger = logger;
        }

        public void SetPlateauSize(string sizeInput)
        {
            var commands = sizeInput.Split(' ');

            if (commands.Length != 2 || !char.IsNumber(commands[0], 0) || !char.IsNumber(commands[1], 0))
                throw new Exception("The size input must consist of 2 numbers, seperated by spaces");
            
            _xSize = Convert.ToInt32(commands[0]);
            _ySize = Convert.ToInt32(commands[1]);

            var message = $"{_xSize} {_ySize}";
            _logger.Log($"Plateau size set: {message}");
        }

        public void AddRover(Rover rover, string initialPos)
        {
            rover.ParsePosition(initialPos);
            var tmpPos = rover.GetTmpPos();
            var commands = tmpPos.Split(' ');
            CheckPositions(Convert.ToInt32(commands[0]), Convert.ToInt32(commands[1]));
            rover.SetPosition();
            rovers.Add(rover);
            _logger.Log($"Rover {rover.Name} added to plateau");

        }

        public void MoveRover(string name, string moveInstructions)
        {
            Rover result = rovers.First(s => s.Name == name);
            result.Move(moveInstructions);
            var tmpPos = result.GetTmpPos();
            var commands = tmpPos.Split(' ');
            CheckPositions(Convert.ToInt32(commands[0]), Convert.ToInt32(commands[1]));
            result.SetPosition();
        }

        public string GetRoverPoition(string name)
        {
            Rover result = rovers.First(s => s.Name == name);
            var msg = result.GetCurrentPos();
            return msg;
        }


        public void CheckPositions(int x, int y)
        {
            if (x < 0 || x > _xSize)
                throw new Exception(
                    $"Cannot set rover to x position {x.ToString()}. This fall outside of the plateau limits");

            if (y < 0 || y > _ySize)
                throw new Exception(
                    $"Cannot set rover to y position {y.ToString()}. This fall outside of the plateau limits");
            
            foreach (var rover in rovers)
            {
                if (rover.X == x && rover.Y == y)
                {
                    throw new Exception(
                    $"Cannot set rover to position {x.ToString()},{y.ToString()}. Another rover already occupies this position.");

                }
            }
        }
    }
}
