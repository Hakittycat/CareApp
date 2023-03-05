using UnityEngine;

public class FullScreenModalManager : MonoBehaviour {
    public CanvasGroup canvasGroup;
    public Animator animator;

    public void CloseWindow() {
        animator.Play("close");
        DisableWindow();
    }

    public void OpenWindow() {
        animator.Play("open");
        EnableWindow();
    }

    private void EnableWindow() {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void DisableWindow() {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}