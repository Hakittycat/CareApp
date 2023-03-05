using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class NavigationButton : MonoBehaviour {
    public string scene;
    public Color notSelected;
    public Color selected;

    private void Start() {
        UpdateColor();
        GetComponent<Button>().onClick.AddListener(WhenClicked);
    }

    private void WhenClicked() {
        if (!IsActive()) {
            var sceneLoader = FindObjectOfType<SceneLoader>();
            if (sceneLoader != null) {
                sceneLoader.LoadScene(scene);
            }
        }
    }

    private bool IsActive() {
        return SceneManager.GetActiveScene().name.Equals(scene);
    }

    private void UpdateColor() {
        GetComponent<Image>().color = IsActive() ? selected : notSelected;
    }
}