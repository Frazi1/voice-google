using GoogleSpeechApi.Context.Interfaces;

namespace GoogleSpeechApi.Context
{
    public class VariableRepresentation : IIdentifierRepresentation
    {
        public string Name { get; }
        public string PhoneticName { get; }
        public string NormalizedName { get; set; }

        public VariableRepresentation(string name, string phoneticName, string normalizedName)
        {
            Name = name;
            PhoneticName = phoneticName;
            NormalizedName = normalizedName;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(PhoneticName)}: {PhoneticName}, {nameof(NormalizedName)}: {NormalizedName}";
        }
    }
}