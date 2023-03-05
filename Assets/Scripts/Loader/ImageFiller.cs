using UnityEngine;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour {

    public Image image;
    public float increment;
    private float fill = 0f;

    public void Update() {
        if (fill >= 1f) {
            fill = 0f;
        }
        image.fillAmount = fill;
        fill += increment;
    }
}