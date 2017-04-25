using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void PuffyAction();

public enum EventList {
    PuffyStart, PuffyStop, PuffyIntro, PuffyStory, PuffyTag, PuffyUpdatePos
}

    class EventManager {

        Dictionary<int, PuffyAction> events = new Dictionary<int, PuffyAction>();

        public static EventManager Instance = null;

        public EventManager() {
            Instance = this;
        }

        public void startListening(int eventName, PuffyAction action) {
            events.Add(eventName, action);
        }

        public void stopListening(int eventName) {
            if (events.ContainsKey(eventName)) {
                events.Remove(eventName);
            }
        }

        public void triggerEvent(int eventName) {
            if (events.ContainsKey(eventName)) {
                events[eventName]();
            } else {
                Console.WriteLine("No corresponding element");
            }
        }

    }