using TMPro;
using UnityEngine;

public class PopupMenu : MonoBehaviour {
    public TextMeshProUGUI body;

    private void Start() {
        gameObject.SetActive(false);
    }

    public void show(string text) {
        SetBodyText(text);
        Open();
    }

    private void SetBodyText(string text) {
        body.text = text;
    }

    private void Open() {
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}