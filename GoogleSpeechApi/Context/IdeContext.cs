using System.Collections.Generic;
using System.Linq;
using GoogleSpeechApi.Context.Interfaces;
using GoogleSpeechApi.Context.Provider;

namespace GoogleSpeechApi.Context
{
    public class IdeContext : IIdeContext
    {
        private IVariableProvider VariableProvider { get; }

        public IdeContext(IVariableProvider variableProvider)
        {
            VariableProvider = variableProvider;
        }

        public ICollection<VariableRepresentation> GetVariables()
        {
            return VariableProvider.GetVariables().ToList();
        }
    }
}