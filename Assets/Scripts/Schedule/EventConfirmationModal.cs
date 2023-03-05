using UnityEngine;

public class EventConfirmationModal : MonoBehaviour {
    public FullScreenModalManager modal;

    [HideInInspector]
    public EventGridItem confirming;

    public ScheduleEditor editor;

    public void SetEventToConfirm(EventGridItem item) {
        confirming = item;
    }

    public void Open() {
        modal.OpenWindow();
    }

    public void Confirm() {
        editor.Add(confirming.definition.id);
        confirming.selected = true;
        confirming.UpdateUI();
        modal.CloseWindow();
    }

    public void Deny() {
        modal.CloseWindow();
    }
}