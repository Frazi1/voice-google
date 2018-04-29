using System;
using System.Threading.Tasks;

namespace GoogleSpeechApi.Recognizer.Interfaces
{
    public delegate void SpechRecognizerHandler(object s, SpeechRecognizerEventArgs e);

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
        Task StartRecognition();
        void StopRecognition();
    }
}