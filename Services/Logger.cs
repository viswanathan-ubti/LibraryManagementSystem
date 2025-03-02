using System;
using System.IO;

namespace LibraryManagementSystem.Services
{
    public static class Logger
    {
        private static string logFilePath = "log.txt"; // Log file path

        //Log a message to the log file with a timestamp
        public static void Log(string message)
        {
            try
            {
                // Open the log file in append mode and write the log entry
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
}