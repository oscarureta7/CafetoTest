using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CafetoTest
{
    public class TextFileLoggingStrategy : ILoggingStrategy
    {

        private string _filePath;
        public string FilePath { get => _filePath; }

        public TextFileLoggingStrategy(string filePath) {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath parameter cannot be null or empty.");

            _filePath = filePath;
        }

        public bool Log(LogMessageType type, string message)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(_filePath))
                {
                    writer.Write(String.Format("{0} {1} : {2} \n", DateTime.Now, type, message));
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
