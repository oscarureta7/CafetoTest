using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafetoTest
{
    public class ConsoleLoggingStrategy : ILoggingStrategy
    {
        public bool Log(LogMessageType type, string message)
        {
            Console.WriteLine(String.Format("{0} {1} : {2}", DateTime.Now, type, message));
            return true;
        }
    }
}
