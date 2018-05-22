using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Speech.V1;
using GoogleSpeechApi.Context.Provider;
using GoogleSpeechApi.Recognizer.Interfaces;
using NAudio.Wave;

namespace GoogleSpeechApi.Recognizer
{
    public class GoogleSpechRecognizerWrapper : ISpeechRecongizer
    {
        private List<SpeechClient.StreamingRecognizeStream> streams = new List<SpeechClient.StreamingRecognizeStream>();
        private bool _writeMore = true;
        private readonly object _writeLock = new object();
        private readonly IVariableProvider _variableProvider;
        private bool isRecording = false;

        private SpeechClient.StreamingRecognizeStream GetCurrentStream => streams.Last();

        private readonly WaveInEvent _waveInEvent = new WaveInEvent
        {
            DeviceNumber = 0,
            WaveFormat = new WaveFormat(16000, 1)
        };

        private SpeechClient _speechClient;

        public GoogleSpechRecognizerWrapper(IVariableProvider variableProvider)
        {
            _variableProvider = variableProvider;
        }

        private static StreamingRecognizeRequest GetStreamingRecognizeRequest(SpeechContext speechContext)
        {
            var streamingRecognizeRequest = new StreamingRecognizeRequest
            {
                StreamingConfig = new StreamingRecognitionConfig
                {
                    Config = new RecognitionConfig
                    {
                        Encoding =
                            RecognitionConfig.Types.AudioEncoding.Linear16,
                        SampleRateHertz = 16000,
                        LanguageCode = "ru",
                        MaxAlternatives = 0,
                        SpeechContexts = {speechContext}
                    },
                    InterimResults = true,
                }
            };
            return streamingRecognizeRequest;
        }

        private async Task<int> StreamingMicRecognizeAsync()
        {
            try
            {
                if (WaveIn.DeviceCount < 1)
                {
                    throw new ApplicationException("No microphone!");
                }

                _speechClient = SpeechClient.Create();
                var stream = _speechClient.StreamingRecognize();
                streams.Add(stream);
                var speechContext = new SpeechContext();
                speechContext.Phrases.AddRange(new[]
                        {"int", "for", "true", "false", "public", "private", "bool", "static", "void", "переменная"}
                    /*.Concat(_variableProvider.GetVariables().Select(v => v.Name))*/);
                // Write the initial request with the config.
                StreamingRecognizeRequest recognizeRequest = GetStreamingRecognizeRequest(speechContext);
                await stream.WriteAsync(recognizeRequest);
                // Print responses as they arrive.

                Task printResponses = Task.Run(async () =>
                {
                    while (await stream.ResponseStream.MoveNext(default(CancellationToken)))
                    {
                        foreach (StreamingRecognitionResult streamingRecognitionResult in stream
                            .ResponseStream
                            .Current.Results)
                        {
                            if (streamingRecognitionResult.IsFinal)
                            {
                                var transcript = streamingRecognitionResult.Alternatives[0].Transcript;
                                OnSpeechRecognized?.Invoke(this, new SpeechRecognizerEventArgs(transcript));
                            }
                        }
                    }
                });
                // Read from the microphone and stream to API.
                ActivateMicrophone();
                Console.WriteLine("Speak now.");
                //await Task.Delay(TimeSpan.FromSeconds(seconds));
                // Stop recording and shut down.
                //StopRecognition();
                await printResponses;
                //await printResponses;
                return 0;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return -1;
        }

        private void ActivateMicrophone()
        {
            if (!isRecording)
            {
                _waveInEvent.DataAvailable += WriteData;
                _waveInEvent.StartRecording();
                isRecording = true;
            }
        }

        private void WriteData(object sender, WaveInEventArgs args)
        {
            lock (_writeLock)
            {
                if (!_writeMore) return;
                try
                {
                    GetCurrentStream.WriteAsync(
                        new StreamingRecognizeRequest
                        {
                            AudioContent = Google.Protobuf.ByteString
                                .CopyFrom(args.Buffer, 0, args.BytesRecorded),
                        }).Wait();
                }
                catch (Exception e)
                {
                    OnError?.Invoke(this, new SpeechRecognizerErrorEventArgs(e));
                }
            }
        }

        public event SpechRecognizerHandler OnSpeechRecognized;
        public event SpeechRecognizerErrorHandler OnError;

        public void StartRecognitionAsync()
        {
            StreamingMicRecognizeAsync().ConfigureAwait(false);
            //            StreamingMicRecognizeAsync(100);
            OnSpeechRecognized += (o, a) => { Debug.WriteLine(a.Text); };
        }

        public void StopRecognition()
        {
            _waveInEvent.StopRecording();
            lock (_writeLock) _writeMore = false;

            try
            {
                GetCurrentStream.WriteCompleteAsync().Wait();
            }
            catch
            {
                // ignored
            }
        }

        public async Task StartRecognition()
        {
            await StreamingMicRecognizeAsync();
        }
    }
}