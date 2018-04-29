using System;
using System.Threading.Tasks;
using GoogleSpeechApi;
using GoogleSpeechApi.Grammars;
using GoogleSpeechApi.Recognizer;
using GoogleSpeechApi.Recognizer.Interfaces;
using VoiceCodeWpf.Commands;

namespace VoiceCodeWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ISpeechRecongizer a;
        private Task t;
        private readonly Grammar _grammar;

        public MainWindow()
        {
            InitializeComponent();
            _grammar = new Grammar();
            _grammar.Bindings.Add(new GrammarCommandBinding
            {
                GrammarRule = new GrammarRule("Валера ест огурцы"),
                Command = new PrintCommand(AppendToEditor)
            });
        }


        private void Append(object o, SpeechRecognizerEventArgs speechRecognizerEventArgs)
        {
            try
            {
                AppendToEditor(speechRecognizerEventArgs.Text);
            }
            catch (Exception e)
            {
                VoiceCodeWpf.Logging.Logger.Write(e);
            }
        }

        private void AppendToEditor(string text)
        {
            Dispatcher.Invoke(() => { TextEditor.AppendText(text); });
        }

        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var r = new GoogleSpechRecognizerWrapper();
            r.OnSpeechRecognized += (o, args) =>
            {
                _grammar.Execute(args.Text);
            };
            //r.OnSpeechRecognized += (o, args) => { AppendToEditor(args.Text); };
            r.StartRecognitionAsync();
        }
    }
}