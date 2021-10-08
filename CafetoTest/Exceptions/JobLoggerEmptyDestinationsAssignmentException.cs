using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafetoTest.Exceptions
{
    public class JobLoggerEmptyDestinationsAssignmentException : Exception
    {
        public JobLoggerEmptyDestinationsAssignmentException() : base("JobLogger destinations cannot be empty.") { 
        }
    }
}
