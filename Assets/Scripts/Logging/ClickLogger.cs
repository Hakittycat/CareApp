using UnityEngine;
using UnityEngine.EventSystems;

public class ClickLogger : MonoBehaviour, IPointerClickHandler {
    public string data;

    public void OnPointerClick(PointerEventData eventData) {
        var loggingController = FindObjectOfType<PersistentLoggingController>();
        if (loggingController != null) {
            loggingController.LogClick(data);
        }
    }
}