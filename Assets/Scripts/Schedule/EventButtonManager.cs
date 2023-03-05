using UnityEngine;

public class EventButtonManager : MonoBehaviour {

    public ScheduleActionWindow actionWindow;
    public WindowManager windowManager;
    public EventConfiguration eventConfiguration;

    public Sprite GetSprite(ScheduledEvent scheduledEvent) {
        return eventConfiguration.GetDefinition(scheduledEvent.eventId).sprite;
    }

    public void ShowActionWindowFor(ScheduledEvent scheduledEvent) {
        actionWindow.SetSubjectEvent(scheduledEvent);
        windowManager.OpenWindow("Actions");
    }
}