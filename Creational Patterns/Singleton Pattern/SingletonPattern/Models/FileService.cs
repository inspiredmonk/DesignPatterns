using SingletonPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern.Models
{
    public class FileService
    {
        private readonly ILogger _logger;
        public FileService(ILogger logger) {
            _logger = logger;
        }

        public void DoWork(string message) {
            _logger.Log(message);
        }
    }
}
