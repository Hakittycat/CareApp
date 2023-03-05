using UnityEngine;

public class EmptyNotification : MonoBehaviour {

    public ScheduleController scheduleController;

    private void Start() {
        gameObject.SetActive(ShouldBeVisible());
    }

    public void UpdateVisibility() {
        gameObject.SetActive(ShouldBeVisible());
    }

    private bool ShouldBeVisible() {
        return scheduleController.GetSchedule().Count <= 0;
    }
}