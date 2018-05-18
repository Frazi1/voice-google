using System;
using System.IO;
using System.Text;
using GoogleSpeechApi.DI.Interfaces;
using JetBrains.Annotations;

namespace VoiceCodeWpf.Logging
{
    [UsedImplicitly]
    public class Logger : ILogger
    {
        private readonly object _sync = new object();
        private readonly string _pathToLog;

        private string Filename => Path.Combine(_pathToLog, $"{AppDomain.CurrentDomain.FriendlyName}_{DateTime.Now:dd.MM.yyy}.log");


        public Logger()
        {
            _pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (!Directory.Exists(_pathToLog))
                Directory.CreateDirectory(_pathToLog); // Создаем директорию, если нужно
        }

        public void Error(Exception ex)
        {
            try
            {
                string fullText = $"[{DateTime.Now:dd.MM.yyy HH:mm:ss.fff}] [{ex.TargetSite.DeclaringType}.{ex.TargetSite.Name}()] {ex.Message}\r\n";
                WriteText(fullText);
            }
            catch
            {
                // Перехватываем все и ничего не делаем
            }
        }


        private void WriteText(string text)
        {
            lock (_sync)
            {
                File.AppendAllText(Filename, text, Encoding.GetEncoding("Windows-1251"));
            }
        }

        public void Info(string message)
        {
            WriteText(message);
        }
    }
}