using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleView : MonoBehaviour {
    public ScheduleController scheduleController;
    public Transform scheduleGridTransform;
    public GameObject prefab;
    public bool showCompleted;

    private void Start() {
        RedrawScheduleButtons();
        scheduleController.onScheduleChange.AddListener(RedrawScheduleButtons);
    }

    public void OnElementDrop() {
        StartCoroutine(WaitThenUpdatePositions());
    }

    private IEnumerator WaitThenUpdatePositions() {
        yield return new WaitForSeconds(0.25f);
        UpdatePositions();
    }

    public void UpdatePositions() {
        var filtered = GetEventButtonGameObjects();
        for (var i = 0; i < filtered.Count; i++) {
            var child = filtered[i];
            var component = child.GetComponent<ScheduledEventButton>();
            component.GetSubject().SetPosition(i);
        }
    }

    private List<GameObject> GetEventButtonGameObjects() {
        var filtered = new List<GameObject>();
        for (var i = 0; i < scheduleGridTransform.childCount; i++) {
            var child = scheduleGridTransform.GetChild(i).gameObject;
            if (child.GetComponent<ScheduledEventButton>() != null) {
                filtered.Add(child);
            }
        }

        return filtered;
    }

    public void RedrawScheduleButtons() {
        ClearChildren();
        var items = scheduleController.GetSchedule();
        items.Sort((e1, e2) => e1.GetPosition().CompareTo(e2.GetPosition()));
        foreach (var scheduledEvent in items) {
            if (!showCompleted && scheduledEvent.IsCompleted()) continue;
            CreateEventButton(scheduledEvent);
        }
    }

    private void ClearChildren() {
        for (var i = 0; i < scheduleGridTransform.childCount; i++) {
            Destroy(scheduleGridTransform.GetChild(i).gameObject);
        }
    }

    private void CreateEventButton(ScheduledEvent scheduledEvent) {
        var obj = Instantiate(prefab, scheduleGridTransform);
        var component = obj.GetComponent<ScheduledEventButton>();
        component.SetSubject(scheduledEvent);
    }
}