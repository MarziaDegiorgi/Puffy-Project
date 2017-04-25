using System;
using Microsoft.Kinect;
//using Microsoft.Speech.AudioFormat;
//using Microsoft.Speech.Recognition;
using System.Speech.Recognition;
using System.Speech.AudioFormat;

namespace PuffyProject
{
   public class AudioRecognition
    {
        private KinectSensor sensor;
        private static string name;

        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// </summary>
        private SpeechRecognitionEngine speechEngine;
        private RecognizerInfo recInf;

        /// <summary>
        /// before creating this class the kinect sensor must be initialize
        /// </summary>
        public AudioRecognition(KinectSensor sensor)
        {
            this.sensor = sensor;
            this.recInf = GetKinectRecognizer();
            name = null;
            if (recInf != null)
            {
                this.speechEngine = new SpeechRecognitionEngine(recInf.Id);
            }
        }

        /// <summary>
        /// build the dictionary with the words important for recognizing the speech
        /// </summary>
        private void buildGrammar()
        {
            var commands = new Choices();
            commands.Add(new SemanticResultValue("ciao", "CIAO"));
            commands.Add(new SemanticResultValue("buongiorno", "CIAO"));
            commands.Add(new SemanticResultValue("arrivederci", "ARRIVEDERCI"));
            commands.Add(new SemanticResultValue("grazie", "GRAZIE"));
            commands.Add(new SemanticResultValue("abbraccio", "ABBRACCIO"));
            commands.Add(new SemanticResultValue("si", "SI"));
            commands.Add(new SemanticResultValue("no", "NO"));

            try {
                var gb = new GrammarBuilder { Culture = recInf.Culture };
                gb.Append(commands);

                var g = new Grammar(gb);
                speechEngine.LoadGrammar(g);

                //fires the events
                speechEngine.SpeechRecognized += speechRecognizer;

                speechEngine.SetInputToAudioStream(
                       sensor.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);

            } catch {
                Console.WriteLine("No Kinect");
            }
                      
        }

        /// <summary>
        /// Gets the metadata for the speech recognizer (acoustic model) most suitable to
        /// process audio from Kinect device.
        /// </summary>
        /// <returns>
        /// RecognizerInfo if found, <code>null</code> otherwise.
        /// </returns>
        private static RecognizerInfo GetKinectRecognizer()
        {
            foreach (RecognizerInfo recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return recognizer;
                }
            }

            return null;
        }

        /// <summary>
        /// Handler for recognized speech events
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void speechRecognizer(object sender, SpeechRecognizedEventArgs e)
        {
            // Speech utterance confidence below which we treat speech as if it hadn't been heard
            const double ConfidenceThreshold = 0.3;

            if (e.Result.Confidence >= ConfidenceThreshold)
            {
                switch (e.Result.Semantics.Value.ToString())
                {
                    case "CIAO":
                        PuffySpeak.Instance.speak("ciao,sono puffy, come ti chiami?", -1);
                        break;

                    case "ARRIVEDERCI":
                        
                        if (name != null)
                        {
                            PuffySpeak.Instance.speak("ciao ciao " + name + " ,ci vediamo presto", -1);
                        }else
                        {
                            PuffySpeak.Instance.speak("ciao ciao ,ci vediamo presto", -1);
                        }
                        break;

                    case "NO":
                        PuffySpeak.Instance.speak("magari la prossima volta", -1);
                        break;

                    case "SI":
                        PuffySpeak.Instance.speak("perfetto, iniziamo", -1);
                        break;

                    case "GRAZIE":
                        PuffySpeak.Instance.speak("prego mio piccolo amico", -1);
                        break;
                    case "ABBRACCIO":
                        PuffySpeak.Instance.speak("vieni qui amo", -1);
                        break;
                }
            }
        }

        /// <summary>
        /// called after the initialization of kinect, to start Audio Recognition
        /// </summary>
        public void startAudioRecognition()
        {
            this.buildGrammar();
        }

        /// <summary>
        /// called before stopping puffy 
        /// </summary>
        public void stopAudioRecognition()
        {
            if (null != this.speechEngine)
            {
                this.speechEngine.SpeechRecognized -= speechRecognizer;
                this.speechEngine.RecognizeAsyncStop();
            }
        }
    }
}

