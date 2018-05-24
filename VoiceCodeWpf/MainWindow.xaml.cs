using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using GoogleSpeechApi.Context.Interfaces;
using GoogleSpeechApi.DI;
using GoogleSpeechApi.Grammars;
using GoogleSpeechApi.Grammars.Config;
using GoogleSpeechApi.Recognizer;
using GoogleSpeechApi.Recognizer.Interfaces;
using ICSharpCode.AvalonEdit;
using Ninject;
using VoiceCodeWpf.Commands;
using VoiceCodeWpf.DI;

namespace VoiceCodeWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ISpeechRecongizer _recongizer = Kernel.Instance.Get<GoogleSpechRecognizerWrapper>();
        private readonly Grammar _grammar;
        private string lastRecognized;
        private bool subscribed = false;

        public MainWindow()
        {
            InitializeComponent();
            Kernel.Instance.Load(new DI.DiModule());

            _grammar = new Grammar(Kernel.Instance.Get<IIdeContext>());
            //_grammar.Rules.Add(GrammarRuleBuilder.Get
            //    .FromString("Валера ест огурцы")
            //    .WithCommand(new PrintCommand(AppendCode))
            //    .Build());
            //_grammar.Rules.Add(GrammarRuleBuilder.Get
            //    .FromString("переменная <text>")
            //    .WithCommand(new PrintCommand(AppendCode))
            //    .Build());
            //
            _grammar.Rules.Add(GrammarRuleBuilder.Get
                .FromString("<text>")
                .WithCommand(new PrintCommand(AppendCode))
                .Build());
        }

        private void AppendCode(string text)
        {
            lastRecognized = text;
            AppendToEditor(CodeEditor, text + Environment.NewLine);
        }

        private void AppendToEditor(TextEditor editor, string text)
        {
            Dispatcher.Invoke(() => { editor.AppendText(text); });
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!subscribed)
            {
                _recongizer.OnSpeechRecognized += (o, args) =>
                {
                    AppendToEditor(TextEditorHistory, args.Text + Environment.NewLine);
                    _grammar.Execute(args.Text + Environment.NewLine);
                };
                _recongizer.OnError += (o, args) => { AppendToEditor(TextEditorHistory, args.Exception.ToString()); };
                subscribed = true;
            }

//r.OnSpeechRecognized += (o, args) => { AppendToEditor(args.Text); };
            _recongizer.StartRecognitionAsync();
        }

        private void EvalRecognitionEventHandle(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(lastRecognized))
            {
                if (e.Key == Key.Right)
                    File.AppendAllLines(@"Log/correct.txt", new[] {lastRecognized});
                if (e.Key == Key.Left)
                    File.AppendAllLines(@"Log/incorrect.txt", new[] {lastRecognized});
            }
        }
    }
}