using System;
using Microsoft.Kinect;

namespace PuffyProject
{
    public class EmotionRecognition
    {
        KinectSensor kinect;
        private byte[] colorPixel;

        /// <summary>
        /// Frame to be analyzed.
        /// FrameDetector to analyze the stream.
        /// Emotion and Engagement levels to be updated each frame and then returned.
        /// </summary>
        private Affdex.Frame _frame;
        private Affdex.FrameDetector _frameDetector;
        private String emotion;
        private float engagement;
        private String resultingEmotion;

        public EmotionRecognition(KinectSensor kinect) {
            this.kinect = kinect;
        }

        /// <summary>
        /// to call before enable the skeleton tracking and after the creation of the sensor
        /// </summary>
        public void enableCamera() {
            if (this.kinect != null) {
                this.kinect.ColorStream.Enable();
                //event that fires when a colour frame arrived
                this.kinect.ColorFrameReady += this.SensorColorFrameReady;
            }
        }

        private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e) {
            colorPixel = new byte[this.kinect.ColorStream.FramePixelDataLength];
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame()) {
                if (colorFrame != null) {
                    colorFrame.CopyPixelDataTo(this.colorPixel);
                    _frameDetector = new FrameDetector(30, 30, 1, FaceDetectorMode.SMALL_FACES);
                    InitializeAffdex(_frameDetector);
                    resultingEmotion = FrameAnalysis(colorFrame);
                    Console.WriteLine(resultingEmotion);
                }
            }
        }

        //Methods for emotion analysis

        /// <summary>
        /// Initializes the Detector.
        /// Check the classifierPath var is correct when implementing on minipc.
        /// </summary>
        /// <param name="_frameDetector"></param>
        private void InitializeAffdex(FrameDetector _frameDetector) {
            String classifierPath = "C:\\Program Files(x86)\\Affectiva\\Affdex SDK\\data";
            _frameDetector.setClassifierPath(classifierPath);

            _frameDetector.setImageListener(this);
            _frameDetector.setDetectAllEmotions(true);
            _frameDetector.setDetectAllExpressions(true);
            _frameDetector.setDetectAllEmojis(true);
            _frameDetector.setDetectAllAppearances(true);

            _frameDetector.start();

        }

        /// <summary>
        /// Useless? Needed 'cos of interface.
        /// </summary>
        /// <param name="frame"></param>
        void ImageListener.onImageCapture(Frame frame) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Extracts the emotion data from the face that's being analyzed.
        /// Calls ResultingEmotion and ResultingEngagement to compute deductions.
        /// </summary>
        /// <param name="faces"></param>
        /// <param name="frame"></param>
        void ImageListener.onImageResults(Dictionary<int, Face> faces, Frame frame) {
            float anger = faces[0].Emotions.Anger;
            float contempt = faces[0].Emotions.Contempt;
            float disgust = faces[0].Emotions.Disgust;
            float fear = faces[0].Emotions.Fear;
            float joy = faces[0].Emotions.Joy;
            float sadness = faces[0].Emotions.Sadness;
            float surprise = faces[0].Emotions.Surprise;

            float engagement = faces[0].Emotions.Engagement;

            ResultingEmotion(anger, contempt, disgust, fear, joy, sadness, surprise);
            ResultingEngagement(engagement);
        }

        /// <summary>
        /// This needs to be called each frame by the kinect.
        /// Expects a ColorImageFrame as input.
        /// Processes the frame and returns the current emotion and engagement level
        /// and returns them as strings:
        /// the first field of the array is the name of the prevalent emotion;
        /// the second field of the array is the engagement level float ToString.
        /// </summary>
        /// <param name="colorFrame"></param>
        /// <returns></returns>
        public String[] FrameAnalysis(ColorImageFrame colorFrame) {
            byte[] pixels = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(pixels);

            Affdex.Frame _frame = new Affdex.Frame(colorFrame.Width, colorFrame.Height, pixels, Affdex.Frame.COLOR_FORMAT.BGR);
            _frameDetector.process(_frame);

            String[] toReturn = new String[] { emotion, engagement.ToString() };
            Console.WriteLine(toReturn);
            return toReturn;
        }

        /// <summary>
        /// Compares the levels for each emotion detected to undertand which one is the prevalent.
        /// </summary>
        /// <param name="anger"></param>
        /// <param name="contempt"></param>
        /// <param name="disgust"></param>
        /// <param name="fear"></param>
        /// <param name="joy"></param>
        /// <param name="sadness"></param>
        /// <param name="surprise"></param>
        private void ResultingEmotion(float anger, float contempt, float disgust, float fear, float joy, float sadness, float surprise) {
            float[] emotions = new float[] { anger, contempt, disgust, fear, joy, sadness, surprise };
            foreach (float e in emotions) {
                if (anger >= e) this.emotion = "Anger";
                else if (contempt >= e) this.emotion = "Contempt";
                else if (disgust >= e) this.emotion = "Disgust";
                else if (fear >= e) this.emotion = "Fear";
                else if (joy >= e) this.emotion = "Joy";
                else if (sadness >= e) this.emotion = "Sadness";
                else if (surprise >= e) this.emotion = "Surprise";
                else this.emotion = "ERROR";
            }
        }

        /// <summary>
        /// Assigns the engagement level to this class' paramenter.
        /// </summary>
        /// <param name="engagement"></param>
        private void ResultingEngagement(float engagement) {
            this.engagement = engagement;
        }


    }
}
