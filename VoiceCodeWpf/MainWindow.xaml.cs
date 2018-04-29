using System;
using System.Threading.Tasks;
using GoogleSpeechApi;
using GoogleSpeechApi.Grammars;
using GoogleSpeechApi.Grammars.Config;
using GoogleSpeechApi.Recognizer;
using GoogleSpeechApi.Recognizer.Interfaces;
using ICSharpCode.AvalonEdit;
using VoiceCodeWpf.Commands;

namespace VoiceCodeWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ISpeechRecongizer _recongizer = new GoogleSpechRecognizerWrapper();
        private Task t;
        private readonly Grammar _grammar;

        public MainWindow()
        {
            InitializeComponent();
            _grammar = new Grammar();
            var builder = new GrammarRuleBuilder();
            _grammar.Bindings.Add(new GrammarCommandBinding
            {
                GrammarRule = builder.FromString("Валера ест огурцы"),
                Command = new PrintCommand(AppendCode)
            });
        }

        private void AppendCode(string text)
        {
            AppendToEditor(CodeEditor, text);
        }

        private void AppendToEditor(TextEditor editor, string text)
        {
            Dispatcher.Invoke(() => { editor.AppendText(text); });
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _recongizer.OnSpeechRecognized += (o, args) =>
            {
                AppendToEditor(TextEditorHistory, args.Text + Environment.NewLine);
                _grammar.Execute(args.Text);
            };
            //r.OnSpeechRecognized += (o, args) => { AppendToEditor(args.Text); };
            _recongizer.StartRecognitionAsync();
        }
    }
}