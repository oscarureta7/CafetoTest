using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafetoTest
{
    public interface ILoggingStrategy
    {
        bool Log(LogMessageType type,  string message);
    }
}
