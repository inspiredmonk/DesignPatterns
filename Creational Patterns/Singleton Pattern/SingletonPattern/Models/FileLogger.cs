using SingletonPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern.Models
{
    public class FileLogger: ILogger
    {
        private readonly static FileLogger _instance = new FileLogger();
        private static readonly object _lock = new();
        private readonly string _logFilePath = "app.log";
        private FileLogger()
        { 
            File.WriteAllText(_logFilePath, $"--- Log started at {DateTime.Now} ---\n");
        }

        public void Log(string message) {
            lock (_lock)
            {
                var logEntry = $"[{DateTime.Now:HH:mm:ss}] [Thread-{Thread.CurrentThread.ManagedThreadId}] {message}\n";
                File.AppendAllText(_logFilePath, logEntry);
            }
        }

        public static FileLogger Instance => _instance;
    }
}
