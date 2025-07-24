using SingletonPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    public sealed class SingletonPattern
    {
        private readonly static SingletonPattern _instance = new SingletonPattern();
        private static readonly object _lock = new();

        private SingletonPattern()
        {
            
        }

        public void Log (ILogger _logger,string message)
        {
            lock (_lock)
            {
                _logger.Log(message);
            }
        }

        
    }
}
