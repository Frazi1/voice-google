using System;
using System.Diagnostics;
using GoogleSpeechApi.Commands.Interfaces;

namespace GoogleSpeechApi.Commands
{
    public class ConsoleCommand : ICommand
    {
        public void Execute(string text)
        {
            Debug.WriteLine(text);
            Console.WriteLine(text);
        }
    }
}