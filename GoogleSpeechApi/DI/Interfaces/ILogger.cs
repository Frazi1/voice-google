using System;

namespace GoogleSpeechApi.DI.Interfaces
{
    public interface ILogger
    {
        void Error(Exception e);
        void Info(string message);
    }
}