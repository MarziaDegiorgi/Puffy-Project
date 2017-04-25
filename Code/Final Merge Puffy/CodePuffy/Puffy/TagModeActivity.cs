using System;

class TagModeActivity
    {
    protected string activityName = "Tag Mode Activity";

    public void run() // Runs the Tag activity. For now, it does not do anything but exiting...
    {
        Console.WriteLine("Hey! I'm the {0} and I'm running!", activityName);
        Console.WriteLine("Press something to stop!");
        string something = Console.ReadLine();
        Console.WriteLine("Activity stopped!");
    }
    }