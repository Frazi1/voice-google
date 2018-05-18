using GoogleSpeechApi.DI.Interfaces;
using Ninject.Modules;
using VoiceCodeWpf.Logging;

namespace VoiceCodeWpf.DI
{
    public class DiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<Logger>();
        }
    }
}