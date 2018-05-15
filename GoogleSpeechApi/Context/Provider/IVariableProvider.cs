using System.Collections.Generic;

namespace GoogleSpeechApi.Context.Provider
{
    public interface IVariableProvider
    {
        IEnumerable<VariableRepresentation> GetVariables();
    }
}