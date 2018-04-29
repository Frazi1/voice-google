using System;
using System.Threading.Tasks;
using GoogleSpeechApi;

namespace VoiceCodeWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ISpeechRecongizer a;
        private Task t;

        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void Append(object o, SpeechRecognizerEventArgs speechRecognizerEventArgs)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    TextEditor.AppendText(speechRecognizerEventArgs.Text);
                });
            }
            catch (Exception e)
            {
                VoiceCodeWpf.Logging.Logger.Write(e);
            }
        }

        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var r = new GoogleSpechRecognizerWrapper();
            r.OnSpeechRecognized += (o, args) =>
              {
                  Append(sender, args);
              };
            r.StartRecognitionAsync();

        }
    }
}