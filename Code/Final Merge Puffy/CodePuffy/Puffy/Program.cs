using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace PuffyProject {
    //Main of the Program : Connect the Server & client Tcp and the Arduino.
    class Program {

       public static Queue<int> eventQueue = new Queue<int>();

       public void Run() {

            EventManager eventManager = new EventManager();
            TcpServer server = new TcpServer(eventQueue); //Create the Tcp 
            Sensor kinect = new Sensor(); //Create the kinect sensor 
            EmotionRecognition emotion = new EmotionRecognition(kinect.getKinect());
            SkeletonTracking skelTracking = new SkeletonTracking(kinect.getKinect()); // start tracking the skeleton 
            skelTracking.startTracking();
            AudioRecognition audio = new AudioRecognition(kinect.getKinect());
            kinect.startSensor();
            audio.startAudioRecognition();

            Arduino arduino = new Arduino(skelTracking); //Create the arduino interface          
            PuffySpeak puffySpeak = new PuffySpeak();
            ActivitySelector activitySelector = new ActivitySelector();
            Thread.Sleep(5000);
            try {
                if (arduino.ConnexionArduino()) {
                    server.sendMessage("Arduino connected");
                } else {
                    server.sendMessage("Arduino can't connect");
                }
                Console.WriteLine("Arduino OK");


                while (true) {
                    if (eventQueue.Count > 0) {
                        int e = eventQueue.Dequeue();
                        EventManager.Instance.triggerEvent(e);
                    }
                }

                //Can't detect the Tcp Client (Interface Puffy)
            } catch {
                Console.WriteLine("No client");
            }
        }
    }
}
