using UnityEngine;

public class GoToOverview : MonoBehaviour {
    public WindowManager windowManager;
    public SectionOverview container;

    public void RedirectToOverview() {
        windowManager.OpenWindow("Overview");
        container.Populate();
    }
}