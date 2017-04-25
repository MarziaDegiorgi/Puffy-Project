using System;
using System.IO.Ports;
using System.Threading;
using System.IO;

namespace PuffyProject {
    public class Arduino {

        private SerialPort currentPort = new SerialPort();
        private int bound = 57600;
        byte[] defaultTime = new byte[1]; //Arduino default time of doing action in byte not initilize
        SkeletonTracking kinect;
        public static Arduino Instance = null;

        public Arduino(SkeletonTracking skelTracking) {
            kinect = skelTracking;
            Instance = this;
            currentPort.PortName = "COM5";
            currentPort.Close();
            currentPort.BaudRate = bound;
            startPuffy();
            EventManager.Instance.startListening((int)EventList.PuffyStop, stop);
            EventManager.Instance.startListening((int)EventList.PuffyStart, start);
            EventManager.Instance.startListening((int)EventList.PuffyUpdatePos, updatePos);

        }

        public void stop() {
            stopPuffy();
        }

        public void start() {
            startPuffy();
        }

        public void updatePos() {
            puffyMove();
        }


        //Connexion to the Arduino : Open the port found 
        public bool ConnexionArduino() {
            try {
                if (currentPort.IsOpen == false) {
                    currentPort.Open();
                    return true;
                }
                return true;
            } catch {
                return false;
            }
        }

        //Start Puffy : Start the Roomba and go forward: command send to Arduino : 1
        public string startPuffy() {
            string returnMessage = "cmd not sent";
            try {
                byte[] start = new byte[1]; //Arduino talk in byte
                start[0] = Convert.ToByte(1);
                currentPort.Write(start, 0, 1);
                //defaultTime[0] = Convert.ToByte(5); //default time set to 5s (temporary way to give it)
                //currentPort.Write(defaultTime, 0, 1);
                returnMessage = currentPort.ReadLine(); //send back the Arduino response to the client
            } catch {
                returnMessage = "connexion problem";
            }
            return returnMessage;
        }

        //Stop Puffy : Stop the Roomba motors : command send to Arduino : 0
        public string stopPuffy() {
            string returnMessage = "cmd not sent";
            try {
                byte[] stop = new byte[1]; //Arduino talk in byte
                stop[0] = Convert.ToByte(0);
                currentPort.Write(stop, 0, 1);
                //defaultTime[0] = Convert.ToByte(5);
                //currentPort.Write(defaultTime, 0, 1);
                returnMessage = currentPort.ReadLine(); //send back the Arduino response to the client
            } catch {
                returnMessage = "Can't stop puffy, connexion problem";
            }
            return returnMessage;
        }

        //Start Puffy : Go forward: command send to Arduino : 2
        public string goPuffy() {
            string returnMessage = "cmd not sent";
            try {
                byte[] start = new byte[1]; //Arduino talk in byte
                start[0] = Convert.ToByte(2);
                currentPort.Write(start, 0, 1);
                //defaultTime[0] = Convert.ToByte(5); //default time set to 5s (temporary way to give it)
                //currentPort.Write(defaultTime, 0, 1);
                returnMessage = currentPort.ReadLine(); //send back the Arduino response to the client
            } catch {
                returnMessage = "connexion problem";
            }
            return returnMessage;
        }


        //Turn right Puffy : Set the Roomba motors to turn right : command send to Arduino : 3
        public string turnRPuffy() {
            string returnMessage = "cmd not sent";
            try {
                byte[] stop = new byte[1];
                stop[0] = Convert.ToByte(3);
                currentPort.Write(stop, 0, 1);
                //defaultTime[0] = Convert.ToByte(2);
                //currentPort.Write(defaultTime, 0, 1);
                returnMessage = currentPort.ReadLine();
            } catch {
                returnMessage = "Can't turnR puffy, connexion problem";
            }
            return returnMessage;
        }
        //Turn left Puffy : Set the Roomba motors to turn left : command send to Arduino : 4
        public string turnLPuffy() {
            string returnMessage = "cmd not sent";
            try {
                byte[] stop = new byte[1];
                stop[0] = Convert.ToByte(4);
                currentPort.Write(stop, 0, 1);
                //defaultTime[0] = Convert.ToByte(2);
                //currentPort.Write(defaultTime, 0, 1);
                returnMessage = currentPort.ReadLine();
            } catch {
                returnMessage = "Can't turnL puffy, connexion problem";
            }
            return returnMessage;
        }

        //Go back Puffy : Set the Roomba motors to go backward : command send to Arduino : 5
        public string back() {
            string returnMessage = "cmd not sent";
            try {
                byte[] stop = new byte[1];
                stop[0] = Convert.ToByte(5);
                currentPort.Write(stop, 0, 1);
                //defaultTime[0] = Convert.ToByte(5);
                //currentPort.Write(defaultTime, 0, 1);
                returnMessage = currentPort.ReadLine();
            } catch {
                returnMessage = "Can't go back, connexion problem";
            }
            return returnMessage;
        }

        public int puffyMove() {
            double distance = kinect.getDistance();

            if (distance != 0) {
                while (Math.Abs(distance) > 1f && Math.Abs(distance) < 4f) {
                    //far but not too much
                    while (Math.Abs(kinect.getAngle()) > Math.PI / 8) { // big angle
                        if ((kinect.getX() > 0) && (Math.Abs(kinect.getAngle()) > Math.PI / 8)) {
                            //left and big angle in radiant
                            turnLPuffy();
                        } else if ((kinect.getX() < 0) && (Math.Abs(kinect.getAngle()) > Math.PI / 8)) { //right and big angle in radiant
                            turnRPuffy();
                        }
                    }
                    Thread.Sleep(3000);
                    goPuffy();
                    Thread.Sleep(3000);
                }
                stopPuffy();
                return 1;
            } else {
                stopPuffy();
                return 0;
            }
        }
    }
}