using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafetoTest.Exceptions
{
    public class JobLoggerNoDestinationsSetException : Exception
    {
        public JobLoggerNoDestinationsSetException() : base("JobLogger requires at least one destination to log.") { 
        }
    }
}
