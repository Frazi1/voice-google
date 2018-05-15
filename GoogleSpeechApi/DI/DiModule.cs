using GoogleSpeechApi.Context;
using GoogleSpeechApi.Context.Interfaces;
using GoogleSpeechApi.Context.Provider;
using GoogleSpeechApi.Grammars;
using GoogleSpeechApi.Grammars.Preprocessors;
using GoogleSpeechApi.SpeechProcessing;
using GoogleSpeechApi.SpeechProcessing.Interfaces;
using Ninject.Modules;

namespace GoogleSpeechApi.DI
{
    public class DiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPhoneticConverter>().To<Metaphone>();
            Bind<IVariableProvider>().To<FileVariableProvider>().WithConstructorArgument("path", "vars.txt");
            Bind<IIdeContext>().To<IdeContext>().InSingletonScope();
            Bind<TextWildCard>().ToSelf();

            Bind<EnglishToRussianTransliterator>().ToSelf();
        }
    }
}