using System.Collections.Generic;

namespace GoogleSpeechApi.Context.Interfaces
{
    public interface IIdeContext
    {
        ICollection<VariableRepresentation> GetVariables();
    }
}