using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScheduleActionWindow : MonoBehaviour {
    private ScheduledEvent subject;
    public ScheduleController scheduleController;
    public EventConfiguration configuration;
    public WindowManager windowManager;
    public UnityEvent onMarkComplete;
    [InspectorName("Subject Image")] public Image image;

    public void SetSubjectEvent(ScheduledEvent newSubject) {
        subject = newSubject;
        image.sprite = configuration.GetDefinition(newSubject.GetEventId()).sprite;
    }

    public void OnDelete() {
        scheduleController.Remove(subject);
        windowManager.OpenWindow("Schedule");
    }

    public void OnMarkComplete() {
        scheduleController.MarkComplete(subject);
        onMarkComplete.Invoke();
    }
}