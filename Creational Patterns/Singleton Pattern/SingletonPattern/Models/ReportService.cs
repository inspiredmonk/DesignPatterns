using SingletonPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern.Models
{
    public class ReportService
    {
        private readonly ILogger _logger;
        public ReportService(ILogger logger)
        {
            _logger = logger;
        }
        public void GenerateReport(string message)
        {
            _logger.Log(message);
        }
    }
}
