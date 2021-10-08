using CafetoTest;
using CafetoTest.Exceptions;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

// NOTE: Not all desired test cases were added due to time constraints.
// NOTE: Singleton implementation is done by IoC in client.

namespace CafetoTestUnitTests
{
    [TestClass]
    public class JobLoggerTest
    {
        private const string TextFilePath = "log.txt";
        private const string TestMessage = "Test Message";
        private const string WarningMessage = "Test Warning";
        private const string ErrorMessage = "Test Error";


        private static readonly ILoggingStrategy[] AllLoggingConfigurations = new ILoggingStrategy[] {
            new TextFileLoggingStrategy(TextFilePath),
            new ConsoleLoggingStrategy()
        };

        private static readonly ILoggingStrategy[] TextOnlyLoggingConfigurations = new ILoggingStrategy[] {
            new TextFileLoggingStrategy(TextFilePath)
        };

        private static readonly ILoggingStrategy[] ConsoleOnlyLoggingConfigurations = new ILoggingStrategy[] {
            new ConsoleLoggingStrategy()
        };

        private static readonly LogMessageType[] AllMessageTypesConfiguration = new LogMessageType[] {
            LogMessageType.Error, 
            LogMessageType.Info, 
            LogMessageType.Warning 
        };
        private static readonly LogMessageType[] ErrorsOnlyMessageTypesConfiguration = new LogMessageType[] {
            LogMessageType.Error
        };
        private static readonly LogMessageType[] RiskMessageTypesConfiguration = new LogMessageType[] {
            LogMessageType.Error,
            LogMessageType.Warning
        };


        [TestMethod]
        public void Constructor_ShouldInitializeSingleton_WhenInstanceIsAccessed()
        {
            IJobLogger jobLogger = new JobLogger( AllLoggingConfigurations, AllMessageTypesConfiguration);
            Assert.IsNotNull(jobLogger);
        }

        [TestMethod]
        public void Constructor_ShouldSetDestinationsProperty_WhenExecuted()
        {
            IJobLogger jobLogger = new JobLogger(AllLoggingConfigurations, AllMessageTypesConfiguration);
            Assert.IsNotNull(jobLogger.Destinations);
        }

        [TestMethod]
        public void Constructor_ShouldSetMessageTypesConfigurationProperty_WhenExecuted()
        {
            IJobLogger jobLogger = new JobLogger(AllLoggingConfigurations, AllMessageTypesConfiguration);
            Assert.IsNotNull(jobLogger.MessageTypeConfiguration);
        }

        [TestMethod]
        public void LogMessage_ShouldExecuteSuccessfully_WhenLoggerIsCorrectlySetup()
        {
            IJobLogger jobLogger = new JobLogger(AllLoggingConfigurations, AllMessageTypesConfiguration);
            jobLogger.LogMessage(LogMessageType.Info,TestMessage);
            jobLogger.LogMessage(LogMessageType.Error,ErrorMessage);
            jobLogger.LogMessage(LogMessageType.Warning,WarningMessage);
        }

        [TestMethod]
        public void LogMessage_ShouldLogAllDestinationsSuccessfully_WhenAllDestinationsAreSet() {
            IJobLogger jobLogger = new JobLogger(AllLoggingConfigurations, AllMessageTypesConfiguration);

            int total = jobLogger.LogMessage(LogMessageType.Error, ErrorMessage);

            Assert.AreEqual(AllLoggingConfigurations.Length, total);
        }

        [TestMethod]
        public void LogMessage_ShouldLogToText_WhenTextFileDestinationEnabled()
        {
            IJobLogger jobLogger = new JobLogger(TextOnlyLoggingConfigurations, AllMessageTypesConfiguration);

            int total = jobLogger.LogMessage(LogMessageType.Error, ErrorMessage);

            Assert.AreNotEqual(total, 0);
        }

        [TestMethod]
        public void LogMessage_ShouldLogToConsole_WhenConsoleDestinationEnabled()
        {
            IJobLogger jobLogger = new JobLogger(ConsoleOnlyLoggingConfigurations, AllMessageTypesConfiguration);

            int total = jobLogger.LogMessage(LogMessageType.Error, ErrorMessage);

            Assert.AreNotEqual(total, 0);
        }

        [TestMethod]
        public void LogMessage_ShouldReturnZero_WhenLoggingNonAllowedTypes() {
            IJobLogger jobLogger = new JobLogger(AllLoggingConfigurations, ErrorsOnlyMessageTypesConfiguration);

            int total = 0;

            total += jobLogger.LogMessage(LogMessageType.Info, TestMessage);
            total += jobLogger.LogMessage(LogMessageType.Warning, WarningMessage);

            Assert.AreEqual(total,0);
        }

        [TestMethod]
        public void LogMessage_ShouldReturnZero_WhenNoMessageTypesAreSet()
        {
            IJobLogger jobLogger = new JobLogger(AllLoggingConfigurations, new LogMessageType[] { });

            int total = 0;

            total += jobLogger.LogMessage(LogMessageType.Info, TestMessage);
            total += jobLogger.LogMessage(LogMessageType.Warning, WarningMessage);
            total += jobLogger.LogMessage(LogMessageType.Error, ErrorMessage);

            Assert.AreEqual(total, 0);
        }

        [TestMethod]
        public void Constructor_ShouldThrowNullReferenceException_WhenLoggingDestinationsAreNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () =>  new JobLogger(null, null)
                );
        }

        [TestMethod]
        public void Constructor_ShouldThrowJobLoggerEmptyDestinationsAssignmentException_WhenLoggingDestinationsAreNull()
        {
            Assert.ThrowsException<JobLoggerEmptyDestinationsAssignmentException>(
                () => new JobLogger(new ILoggingStrategy[] { }, AllMessageTypesConfiguration)
                );
        }

        [TestMethod]
        public void Constructor_ShouldThrowArgumentNullException_WhenMessageTypesConfigurationsAreNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new JobLogger(AllLoggingConfigurations, null)
                );
        }

        [TestMethod]
        public void TextFileLoggingStrategy_ShouldThrowArgumentNullException_WhenPathIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new TextFileLoggingStrategy(null)
                ); ;
        }

        [TestMethod]
        public void TextFileLoggingStrategy_ShouldThrowArgumentNullException_WhenPathIsEmpty()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new TextFileLoggingStrategy("")
                ); ;
        }

        //[TestMethod]
        //public void Name()
        //{
        //    IJobLogger jobLogger = new JobLogger(AllLoggingConfigurations, AllMessageTypesConfiguration);


        //}
    }
}