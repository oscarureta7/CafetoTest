using CafetoTest.Exceptions;
using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;

namespace CafetoTest
{
    public class JobLogger : IJobLogger
    {
        // Traditional singleton implementation, not used to enable dependency injection
        #region Singleton Implementation
        //private static Lazy<JobLogger> _instance = new Lazy<JobLogger>(() => new JobLogger());
        //public static JobLogger Instance { get => _instance.Value; }
        #endregion

        // Static members removed to enable varied life cycles (Transient, Scoped & Singleton)
        #region Private Fields
        private bool _initialized;

        private ILoggingStrategy[] _destinationStrategies;
        private LogMessageType[] _allowedMessageTypes;
        #endregion

        #region Properties
        public ILoggingStrategy[] Destinations { get => _destinationStrategies; }
        public LogMessageType[] MessageTypeConfiguration { get => _allowedMessageTypes; }
        #endregion

        public JobLogger(ILoggingStrategy[] destinationList, LogMessageType[] allowedMessageTypes)
        {
            SetDestinations(destinationList);
            SetMessageTypeConfiguration(allowedMessageTypes);
            _initialized = true;
        }

        #region Public Methods
        public int LogMessage(LogMessageType type, string message)
        {
            if (_allowedMessageTypes == null)
                return 0;
            if (_allowedMessageTypes.Length == 0)
                return 0;

            if (!new List<LogMessageType>(_allowedMessageTypes).Contains(type))
                return 0;

            int outputCount = 0;

            if (_destinationStrategies == null)
            {
                throw new JobLoggerNoDestinationsSetException();
            } else
            {
                foreach (ILoggingStrategy destination in Destinations)
                {
                    if(destination.Log(type, message))
                    {
                        ++outputCount;
                    }
                }
            }

            return outputCount;
        }
    
        private void SetDestinations(ILoggingStrategy[] destinationsList)
        {
            if (destinationsList == null)
                throw new ArgumentNullException("destinationsList parameter cannot be null.");
            if (destinationsList.Length == 0)
                throw new JobLoggerEmptyDestinationsAssignmentException();

            _destinationStrategies = destinationsList;
        }
        
        private void SetMessageTypeConfiguration(LogMessageType[] allowedMessageTypes)
        {
            if (allowedMessageTypes == null)
                throw new ArgumentNullException("messageTypeConfiguration parameter cannot be null");

            _allowedMessageTypes = allowedMessageTypes;
        }
        #endregion
    }
}