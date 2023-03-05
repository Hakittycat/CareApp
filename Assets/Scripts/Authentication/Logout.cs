using UnityEngine;
using UnityEngine.EventSystems;

public class Logout : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        var state = FindObjectOfType<ApplicationState>();
        if (state != null) {
            state.ClearSessionData();
            var loader = FindObjectOfType<SceneLoader>();
            loader.LoadScene("Login");
        }
    }
}