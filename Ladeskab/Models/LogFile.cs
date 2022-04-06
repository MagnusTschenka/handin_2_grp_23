using System;
namespace Ladeskab
{
    public class LogFile : ILogFile
    {
        public LogFile()
        {
        }
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public void AppendTextLock(int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm") + ": Skab låst med RFID: {0}", id);
            }
        }

        public void AppendTextUnlock(int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm") + ": Skab låst op med RFID: {0}", id);
            }
        }
    }
}

