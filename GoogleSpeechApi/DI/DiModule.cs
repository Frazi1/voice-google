using GoogleSpeechApi.Context;
using GoogleSpeechApi.Context.Interfaces;
using GoogleSpeechApi.Context.Provider;
using GoogleSpeechApi.Grammars;
using GoogleSpeechApi.Recognizer;
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
            //Bind<IPhoneticConverter>().To<Metaphone>();
            //Bind<IPhoneticConverter>().To<WAMatchRating>().InSingletonScope();
            //Bind<IPhoneticConverter>().To<CaverPhone>().InSingletonScope();
            Bind<IPhoneticConverter>().To<PhonixMetaphoneAdapter>().InSingletonScope(); //best
            //Bind<IPhoneticConverter>().To<MetaphoneRu>().InSingletonScope();
            //Bind<IPhoneticConverter>().To<NoPhonetic>().InSingletonScope();

            Bind<IPhonemeCorrector>().To<SimplePhonemeCorrector>().InSingletonScope();
            Bind<IStringDistanceEvaluator>().To<LevensteinDistanceEvaluator>().InSingletonScope();
            Bind<ITextTransliterator>().To<EnglishToRussianTransliterator>().InSingletonScope();

            //Bind<IVariableProvider>().To<FileVariableProvider>().WithConstructorArgument("path", "vars.txt");
            Bind<IVariableProvider>().To<FileVariableProvider>().WithConstructorArgument("path", @"D:\Programmin\C#\IdentifierExtractor\IdentifierExtractor\bin\Debug\id.txt");
            Bind<IIdeContext>().To<IdeContext>().InSingletonScope();
            Bind<TextWildCard>().ToSelf();

            Bind<EnglishToRussianTransliterator>().ToSelf();
            Bind<GoogleSpechRecognizerWrapper>().ToSelf();
        }
    }
}