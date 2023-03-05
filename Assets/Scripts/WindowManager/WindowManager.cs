using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindowManager : MonoBehaviour
{
    public string startingWindow;
    public List<WindowData> windows;


    private void Start()
    {
        CloseAll();
        OpenWindow(startingWindow);
    }

    public void OpenWindow(string windowName)
    {
        Debug.Log($"Window: {windowName}");
        var window = FindWindow(windowName);
        if (window == null)
        {
            throw new Exception("Invalid window name was provided.");
        }

        CloseAll();
        window.windowGameObject.SetActive(true);
        window.onOpen.Invoke();
    }

    private void CloseAll()
    {
        windows.ForEach(window => window.windowGameObject.SetActive(false));
    }

    private WindowData FindWindow(string windowName)
    {
        return windows.Find(window => window.windowName.Equals(windowName));
    }

    [Serializable]
    public class WindowData
    {
        public string windowName;
        public GameObject windowGameObject;
        public UnityEvent onOpen;
    }
}