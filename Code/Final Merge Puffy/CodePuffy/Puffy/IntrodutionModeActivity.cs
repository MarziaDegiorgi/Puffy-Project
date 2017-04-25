using System;

namespace PuffyProject {
    class IntrodutionModeActivity {
        private bool changeActivity = false;
        protected string activityName = "Introduction Mode Activity";
        public void run() // Runs the Introduction activity. For now, it does not do anything but exiting...
        {
            Console.WriteLine("Hey! I'm the {0} and I'm running!", activityName);
            PuffySpeak.Instance.speak("Ciao !", -1); // work only outside if
            string startMessage = Arduino.Instance.startPuffy();
            TcpServer.Instance.sendMessage(startMessage);


            int result = 0;
            while (result != 1 && !changeActivity) {
                result = Arduino.Instance.puffyMove();
                if (result == 1) {
                    PuffySpeak.Instance.speak("Me quiamo Puffy e tu ?", -1);
                } else {
                    string turnLMessage = Arduino.Instance.turnLPuffy();
                    TcpServer.Instance.sendMessage(turnLMessage);
                }
            }
        }
        public void stop() {
            changeActivity = true;
        }

    }
}






