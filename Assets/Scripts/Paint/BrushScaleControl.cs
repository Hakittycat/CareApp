using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrushScaleControl : MonoBehaviour {
    [Header("Attributes")]
    public int size = 25;

    [Space]
    [Header("Size Boundaries")]
    public int maxSize = 100;

    public int minSize = 10;

    [Space]
    [Header("Resources")]
    public Brush brush;

    public Button increase;
    public Button decrease;
    public TextMeshProUGUI sizeDisplay;

    [Header("Animation Settings")]
    public Animator textAnimator;

    public float waitTimeBeforeTextChange = 0.15f;


    public void Start() {
        increase.onClick.AddListener(IncreaseBrushSize);
        decrease.onClick.AddListener(DecreaseBrushSize);
        SetSize(size);
    }

    private void IncreaseBrushSize() {
        if (size >= maxSize) return;
        SetSizeWithAnimation(++size);
    }

    private void DecreaseBrushSize() {
        if (size <= minSize) return;
        SetSizeWithAnimation(--size);
    }

    private void SetSizeWithAnimation(int newSize) {
        size = newSize;
        brush.radius = newSize;
        textAnimator.Play("shrink");
        StartCoroutine(UpdateText());
    }

    private void SetSize(int newSize) {
        size = newSize;
        brush.radius = newSize;
        sizeDisplay.text = size.ToString();
    }

    private IEnumerator UpdateText() {
        yield return new WaitForSeconds(waitTimeBeforeTextChange);
        sizeDisplay.text = size.ToString();
    }
}