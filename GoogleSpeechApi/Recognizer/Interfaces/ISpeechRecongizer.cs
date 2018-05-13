using System;
using System.Threading.Tasks;

namespace GoogleSpeechApi.Recognizer.Interfaces
{
    public delegate void SpechRecognizerHandler(object s, SpeechRecognizerEventArgs e);

    public delegate void SpeechRecognizerErrorHandler(object s, SpeechRecognizerErrorEventArgs e);

    public class SpeechRecognizerErrorEventArgs: EventArgs
    {
        public SpeechRecognizerErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; }
    }

    public class SpeechRecognizerEventArgs : EventArgs
    {
        public string Text { get; }

        public SpeechRecognizerEventArgs(string text)
        {
            Text = text;
        }

    }
    
    public interface ISpeechRecongizer
    {
        event SpechRecognizerHandler OnSpeechRecognized;
        event SpeechRecognizerErrorHandler OnError;
        Task StartRecognition();
        void StopRecognition();
        void StartRecognitionAsync();
    }
}