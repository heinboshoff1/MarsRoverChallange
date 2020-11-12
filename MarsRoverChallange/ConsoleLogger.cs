using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverChallange
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _name;
        private readonly string _path;

        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now} : {message}{Environment.NewLine}");
        }
    }
}
