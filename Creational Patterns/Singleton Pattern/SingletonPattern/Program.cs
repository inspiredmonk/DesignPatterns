using SingletonPattern.Interfaces;
using SingletonPattern.Models;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger fileServiceInstance = FileLogger.Instance;
            FileService fileService = new FileService(fileServiceInstance);
            fileService.DoWork("This is file service class.");

            ILogger reportServiceInstance = FileLogger.Instance;
            ReportService reportService = new ReportService(reportServiceInstance);
            reportService.GenerateReport("This is report service class.");
        }
    }
}