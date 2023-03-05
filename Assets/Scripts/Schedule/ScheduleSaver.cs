using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScheduleSaver : MonoBehaviour {
    private ApplicationState state;
    public UnityEvent onSaveBefore;
    public UnityEvent onSaveComplete;
    public bool saving;

    private void Awake() {
        state = FindObjectOfType<ApplicationState>();
    }

    public void SaveSchedule(ScheduleController controller) {
        if (state != null && state.isLoggedIn()) {
            if (saving) return;
            saving = true;
            StartCoroutine(SaveCoroutine(controller.GetSchedule()));
        }
    }

    private IEnumerator SaveCoroutine(List<ScheduledEvent> items) {
        onSaveBefore.Invoke();
        var schedule = new PatientSchedule {schedule = items};
        var request = ScheduleRequests.SaveSchedule(state.token, schedule);
        yield return request.SendWebRequest();
        saving = false;
        onSaveComplete.Invoke();
    }
}