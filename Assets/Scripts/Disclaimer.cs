using Michsky.UI.ModernUIPack;
using UnityEngine;

public class Disclaimer : MonoBehaviour {
    
    public ModalWindowManager modalWindowManager;

    private void Start() {
        modalWindowManager.OpenWindow();
        modalWindowManager.onConfirm.AddListener(() => { modalWindowManager.CloseWindow(); });
    }
}