using GoogleSpeechApi.Context;
using GoogleSpeechApi.Context.Interfaces;
using GoogleSpeechApi.Context.Provider;
using GoogleSpeechApi.Grammars;
using GoogleSpeechApi.SpeechProcessing;
using GoogleSpeechApi.TextProcessing.Helpers;
using GoogleSpeechApi.TextProcessing.Interfaces;
using Ninject.Modules;

namespace GoogleSpeechApi.DI
{
    public class DiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPhoneticConverter>().To<Metaphone>();
            Bind<IStringDistanceEvaluator>().To<LevensteinDistanceEvaluator>().InSingletonScope();
            Bind<ITextTransliterator>().To<EnglishToRussianTransliterator>().InSingletonScope();

            Bind<IVariableProvider>().To<FileVariableProvider>().WithConstructorArgument("path", "vars.txt");
            Bind<IIdeContext>().To<IdeContext>().InSingletonScope();
            Bind<TextWildCard>().ToSelf();

            //Bind<EnglishToRussianTransliterator>().ToSelf();
        }
    }
}