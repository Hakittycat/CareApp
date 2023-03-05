using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ColorSelector : MonoBehaviour, IPointerClickHandler {
    public Brush brush;
    public Color color;
    private Button button;

    public void Awake() {
        button = GetComponent<Button>();
    }

    private void Start() {
        button.onClick.AddListener(() => brush.color = color);
    }

    public void OnPointerClick(PointerEventData eventData) {
        GetComponent<Animator>()?.Play("grow");
    }
}