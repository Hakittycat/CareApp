using System.Collections.Generic;
using UnityEngine;

public class ScheduleEditor : MonoBehaviour {
    public ScheduleController scheduleController;
    public WindowManager windowManager;
    private readonly List<string> events = new List<string>();

    public void Add(string eventId) {
        events.Add(eventId);
    }

    public void Remove(string eventId) {
        events.Remove(eventId);
    }

    public bool Contains(string eventId) {
        return events.Contains(eventId);
    }

    public void Submit() {
        scheduleController.AddAll(events);
        events.Clear();
        windowManager.OpenWindow("Schedule");
    }
}