using System;

namespace CafetoTest.Exceptions
{
    public class JobLoggerNotInitializedException : Exception
    {
        public JobLoggerNotInitializedException() : base("JobLogger is not yet initialized")
        {

        }
    }
}