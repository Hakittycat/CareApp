using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WindowButton : MonoBehaviour {
    public string windowName;
    private WindowManager windowManager;
    private Button button;

    private void Start() {
        windowManager = FindObjectOfType<WindowManager>();
        button = GetComponent<Button>();
        if (windowManager == null) {
            throw new Exception("Window manager must be present in scene for a window button!");
        }

        button.onClick.AddListener(Click);
    }

    private void Click() {
        windowManager.OpenWindow(windowName);
    }
}