using System;
using Microsoft.Kinect;

namespace PuffyProject
{
    public class SkeletonTracking
    {
        
        private KinectSensor sensor;
        private double depth;
        private double x;
        private double distance, lastDistance;
        private double angle;
        private double angleSign;
        public EventHandler goForward;

         
        public SkeletonTracking(KinectSensor sensor)
        {
            this.sensor = sensor;
            distance = 0;
            lastDistance = 0;
            x = 0;
            depth = 0;
            angle = 0;
            angleSign = 0;
        }

        /// <summary>
        ///enable the skeleton tracking and event
        /// </summary>
        public void startTracking()
        {
            //turn on the skeleton stream to receive Skeleton frames
            try {
                this.sensor.SkeletonStream.Enable();
                // Turn on the depth stream to receive depth frames
                this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);

                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;
            } catch {
                Console.WriteLine("No Kinect");
            }       

            //start the kinect
            if (sensor != null)
            {
                sensor.Start();
            }
        }

        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }

                foreach (Skeleton skel in skeletons)
                {
                    if (skel.TrackingState != SkeletonTrackingState.NotTracked)
                    {
                        update(skel);

                        if (goForward != null)
                        {
                            goForward(this, new EventArgs());
                        }
                    }
                }
            }
        }

        private void update(Skeleton skeleton)
        {
            depth = skeleton.Position.Z;
            x = skeleton.Position.X;
            distance = Math.Sqrt(Math.Pow(x,2) + Math.Pow(depth,2)); // in meters
            if (Math.Abs(lastDistance - distance) > 0.5) {
                Program.eventQueue.Enqueue((int)EventList.PuffyUpdatePos);
                lastDistance = distance;
            }           

            if (x >= 0){
                angleSign = 1;
            }else{
                angleSign = -1;
            }

            angle = Math.PI/2 - Math.Abs(Math.Asin(depth / distance)); // radiant
            
            Console.WriteLine("depth distance" +depth);
            Console.WriteLine("x distance" + x);
            Console.WriteLine("angle" + angleSign*angle);
            Console.WriteLine("distance"+ distance);
           
        }

        public double getDepth()
        {
            return depth;
        }

        public double getX()
        {
            return x;
        }

        public double getDistance()
        {
            return distance;
        }

        public double getAngle()
        {
            return angle;
        }

    }
}
