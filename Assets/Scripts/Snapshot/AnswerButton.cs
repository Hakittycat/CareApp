using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour {
    
    public AnswerButtonGroup buttonGroup;
    public string answer;
    public TextMeshProUGUI text;
    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(() => buttonGroup.SetAnswer(answer));
        buttonGroup.Register(this);
    }

    public void SetBackgroundColor(Color color) {
        image.color = color;
    }

    public void SetTextColor(Color color) {
        text.color = color;
    }
}