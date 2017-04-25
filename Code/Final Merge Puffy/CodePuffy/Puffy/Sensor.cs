using System;
using Microsoft.Kinect;
using System.IO;

namespace PuffyProject
{
    public class Sensor
    {
        KinectSensor kinect;

        /// <summary>
        /// intialize the kinect sensor 
        /// </summary>
        public Sensor()
        {
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.kinect = potentialSensor;
                    break;
                }
            }
        }

        /// <summary>
        /// start the kinect sensor( to call after having enable the audio and skeleton)
        /// </summary>
        public void startSensor()
        {
            if (null != this.kinect)
            {
                try
                {
                    // Start the sensor!
                    this.kinect.Start();
                }
                catch (IOException)
                {
                    // Some other application is streaming from the same Kinect sensor
                    this.kinect = null;
                }
            }
        }

        public void stopSensor()
        {
            if (kinect != null)
            {
                kinect.Stop();
            }
        }

        public KinectSensor getKinect()
        {
            return kinect;
        }
    }
}

