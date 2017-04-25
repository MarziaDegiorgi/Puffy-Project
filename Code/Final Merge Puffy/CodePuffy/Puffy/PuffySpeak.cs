using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;


namespace PuffyProject {
    class PuffySpeak {
        public static PuffySpeak Instance = null;
        private SpeechSynthesizer synth = null;

        public PuffySpeak() {
            Instance = this;
            synth = new SpeechSynthesizer();
        }

        public void speak(string speech, int rate) {
            if (synth.State == SynthesizerState.Speaking) {
                synth.SpeakAsyncCancelAll();
            }
            synth.Rate = rate;
            synth.Volume = 100;

            synth.SelectVoiceByHints(VoiceGender.Male);
            //synth.SelectVoice("Microsoft Mike");
            synth.SpeakAsync(speech);
        }

        public void pause() {
            synth.Pause();
        }
        //public void subscribeToStateChanged() {
        //    synth.StateChanged += new EventHandler<StateChangedEventArgs>();
        //}

    }
}
