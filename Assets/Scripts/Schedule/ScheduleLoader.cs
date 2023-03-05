using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ScheduleLoader : MonoBehaviour {
    private ApplicationState state;
    public UnityEvent onLoadError;
    public ScheduleController scheduleController;

    private void Awake() {
        state = FindObjectOfType<ApplicationState>();
    }

    private void Start() {
        LoadSchedule();
    }

    private void LoadSchedule() {
        if (state != null && state.isLoggedIn()) {
            StartCoroutine(LoadScheduleCoroutine());
        }
    }

    private IEnumerator LoadScheduleCoroutine() {
        var request = ScheduleRequests.GetSchedule(state.token);
        yield return request.SendWebRequest();
        if (request.error != null) {
            onLoadError.Invoke();
            yield break;
        }

        var patientSchedule = JsonUtility.FromJson<PatientSchedule>(request.downloadHandler.text);
        scheduleController.SetSchedule(patientSchedule.schedule);
    }
}