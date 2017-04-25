using System;
using System.Windows;


namespace PuffyProject {
    class ActivitySelector {
        bool status; // False if busy (is performing an activity), true otherwise...
        int choice = 0; // The choice made by the therapist...
        int currentActivityNumber = 0;  // 0 means no activity, 1 Introduction Mode, 2 Story Mode, 3 Tag Mode...
        IntrodutionModeActivity activityIntro = new IntrodutionModeActivity();
        StoryModeActivity activityStory = new StoryModeActivity();
        TagModeActivity activityTag = new TagModeActivity();

        public ActivitySelector() // Constructor method...
        {
            EventManager.Instance.startListening((int)EventList.PuffyIntro, introductionMode);
            EventManager.Instance.startListening((int)EventList.PuffyStory, storyTelling);
            EventManager.Instance.startListening((int)EventList.PuffyTag, tagMode);

            this.currentActivityNumber = 0;
            this.choice = 0;
            this.status = false;
        }

        public void introductionMode() {
            stopCurrentActivity();
            setCurrentActivity(1);
            setStatus(false);
            Console.WriteLine("Starting Introduction Mode. Performing activity...");
            activityIntro.run();

        }

        public void storyTelling() {
            setCurrentActivity(2);
            setStatus(false);
            Console.WriteLine("Starting Story Mode. Performing activity...");
            activityStory.run();
        }


        public void tagMode() {
            setCurrentActivity(3);
            setStatus(false);
            Console.WriteLine("Starting Tag Mode. Performing activity...");
            activityTag.run();
        }

        public void reset() // Resets command variables to default values...
        {
            setStatus(true);
            setCurrentActivity(0);
            setChoice(0);
        }

        public bool getStatus() // Get function for the status variable...
        {
            return status;
        }

        public void setStatus(bool s) // Set function for the status variable...
        {
            this.status = s;
        }

        public int getChoice() // Get function for the choice variable...
        {
            return choice;
        }

        public void setChoice(int s) // Set function for the choice variable...
        {
            this.choice = s;
        }

        public int getCurrentActivity() // Get function for the currentActivityNumber variable...
        {
            return currentActivityNumber;
        }

        public void setCurrentActivity(int s) // Set function for the currentActivityNumber variable...
        {
            this.currentActivityNumber = s;
        }

        private void stopCurrentActivity() {
            switch (currentActivityNumber) {
                case 1:
                    activityIntro.stop();
                    break;

                case 2:
                    //StoryModeActivity.stop();
                    break;
                case 3:
                    //activityTag.stop();
                    break;
            }
            reset();
        }
    }
}