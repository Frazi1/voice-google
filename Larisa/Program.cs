using System;
using System.Linq;
using System.Threading.Tasks;
using GoogleSpeechApi;
using GoogleSpeechApi.Recognizer;
using GoogleSpeechApi.Recognizer.Interfaces;

namespace Larisa
{
    static class Program
    {
        private static async Task Main(string[] args)
        {
            ISpeechRecongizer r = new GoogleSpechRecognizerWrapper();
            var task = r.StartRecognition();
            Console.WriteLine("started");
            r.OnSpeechRecognized += (o, a) => { Console.WriteLine(a.Text); };
            await task;
        }
    }
}