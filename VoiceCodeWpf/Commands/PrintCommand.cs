using System;
using GoogleSpeechApi.Commands.Interfaces;

namespace VoiceCodeWpf.Commands
{
    public class PrintCommand : ICommand
    {
        private readonly Action<string> _action;
        public PrintCommand(Action<string> append)
        {
            _action = append;
        }
        public void Execute(string param)
        {
            _action.Invoke(param);
        }
    }
}