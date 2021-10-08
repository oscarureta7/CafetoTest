using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafetoTest
{
    public interface IJobLogger
    {
        ILoggingStrategy[] Destinations { get; }
        LogMessageType[] MessageTypeConfiguration { get; }
 
        int LogMessage(LogMessageType type, string message);
    }
}
