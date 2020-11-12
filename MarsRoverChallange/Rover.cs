using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverChallange
{
    public class Rover
    {

        private static List<string> Orientations { get; } = new List<string> { "N", "E", "S", "W" };
        private static List<char> Instructions { get; } = new List<char> { 'L', 'R', 'M' };

        private readonly ILogger _logger;
        public string OrientationCardinal { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string Name { get; set; }
        private string TmpOrientation { get; set; }
        private int TmpX { get; set; }
        private int TmpY { get; set; }

        public Rover(ILogger logger, string name)
        {
            _logger = logger;
            Name = name;

        }

        public void ParsePosition(string positionInput)
        {
            var commands = positionInput.Split(' ');

            if (commands.Length != 3 || !char.IsNumber(commands[0], 0) || !char.IsNumber(commands[1], 0) ||
                !char.IsLetter(commands[2], 0))
                throw new Exception("The position input must consist of 2 numbers and a letter, seperated by spaces");

            if (!Orientations.Contains(commands[2]))
                throw new Exception(
                    $"The last letter of the input string must be a cardinal compass point (case sensitive). E.g. {string.Join(", ", Orientations)}");

            TmpX = Convert.ToInt32(commands[0]);
            TmpY = Convert.ToInt32(commands[1]);
            TmpOrientation= commands[2];

            var message = $"{TmpX} {TmpY} {TmpOrientation}";
        }

        public void SetPosition()
        {

            X = TmpX;
            Y = TmpY;
            OrientationCardinal = TmpOrientation;
            var message = $"{X} {Y} {OrientationCardinal}";
            _logger.Log($"Set Rover position: {message}");
        }

        public string GetCurrentPos()
        {
            var message = $"{X.ToString()} {Y.ToString()} {OrientationCardinal}";
            _logger.Log($"Current Rover position: {message}");
            return message;
        }

        public string GetTmpPos()
        {
            var message = $"{TmpX.ToString()} {TmpY.ToString()}";
            return message;
        }

        //method names must start with capital and parameter is lower case
        public void Move(string instructions)
        {
            foreach (var instruction in instructions)
            {
                if (!Instructions.Contains(instruction))
                    throw new Exception(
                        $"Invalid movement instructions. Commands allowed are: {string.Join(", ", Instructions)} (case sensitive)");
            }

            MovementParser(instructions.ToArray());

            _logger.Log($"Rover moved: {instructions}");
        }

        private void MovementParser(IEnumerable<char> moveInstructions)
        {
            foreach (var m in moveInstructions)
            {
                switch (m)
                {
                    case 'R':
                        TmpOrientation = Rotate(m);
                        break;
                    case 'L':
                        TmpOrientation = Rotate(m);
                        break;
                    case 'M':
                        MoveRover();
                        break;
                }
            }
        }

        private void MoveRover()
        {
            switch (TmpOrientation)
            {
                case "N":
                    TmpY += 1;
                    break;
                case "E":
                    TmpX += 1;
                    break;
                case "S":
                    TmpY -= 1;
                    break;
                case "W":
                    TmpX -= 1;
                    break;
            }
        }

        private string Rotate(char direction)
        {
            int degrees = 0;
            int newdegrees = 0;
            string newCardinal = "";
            switch (TmpOrientation)
            {
                case "N":
                    degrees = (direction == 'R') ? 0 : 360;
                    break;
                case "E":
                    degrees = 90;
                    break;
                case "S":
                    degrees = 180;
                    break;
                case "W":
                    degrees = 270;
                    break;
            }

            switch (direction)
            {
                case 'L':
                    newdegrees = degrees - 90;
                    break;
                case 'R':
                    newdegrees = degrees + 90;
                    break;
            }

            switch (newdegrees)
            {
                case 0:
                case 360:
                    newCardinal = "N";
                    break;
                case 90:
                    newCardinal = "E";
                    break;
                case 180:
                    newCardinal = "S";
                    break;
                case 270:
                    newCardinal = "W";
                    break;
            }
            return newCardinal;
        }

    }
}
