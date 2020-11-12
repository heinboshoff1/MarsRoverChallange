using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverChallange
{
    public class FileLogger : ILogger
    {
        private readonly string _name;
        private readonly string _path;

        public FileLogger(string path, string name)
        {
            _path = path;
            _name = name;
        }

        public void Log(string message)
        {
            File.AppendAllText($@"{_path}\{_name}.log", $"{DateTime.Now} : {message}{Environment.NewLine}");
        }
    }
}
