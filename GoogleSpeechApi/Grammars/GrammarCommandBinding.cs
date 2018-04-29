using GoogleSpeechApi.Commands.Interfaces;

namespace GoogleSpeechApi.Grammars
{
    public class GrammarCommandBinding
    {
        public GrammarRule GrammarRule { get; set; }
        public ICommand Command { get; set; }

        public bool Execute(string input)
        {
            bool match = GrammarRule.Match(input, out string result);
            if (match)
            {
                Command.Execute(result);
                return true;
            }
            return false;
        }
    }
}