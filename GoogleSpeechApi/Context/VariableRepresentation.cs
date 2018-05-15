using GoogleSpeechApi.Context.Interfaces;

namespace GoogleSpeechApi.Context
{
    public class VariableRepresentation : IIdentifierRepresentation
    {
        public string Name { get; }
        public string PhoneticName { get; }

        public VariableRepresentation(string name, string phoneticName)
        {
            Name = name;
            PhoneticName = phoneticName;
        }
    }
}