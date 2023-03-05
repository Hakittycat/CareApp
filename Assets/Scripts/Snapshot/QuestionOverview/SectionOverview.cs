using UnityEngine;
using UnityEngine.Events;

public class SectionOverview : MonoBehaviour {
    public WindowManager windowManager;
    public PromptController promptController;
    public OverviewButtonSettings buttonSettings;
    public GameObject buttonPrefab;
    private PersistentFormData formData;

    private void Awake() {
        formData = FindObjectOfType<PersistentFormData>();
    }

    public void Populate() {
        RemoveChildren();
        Populate(promptController.GetActiveSection());
    }

    private void Populate(QuestionnaireSection section) {
        var orderedList = section.getQuestionDetailsList();
        var index = 0;
        foreach (var question in orderedList) {
            var obj = Instantiate(buttonPrefab, transform);
            var component = obj.GetComponent<QuestionOverviewButton>();
            component.buttonSettings = buttonSettings;
            component.SetLabelText(CreateQuestionLabel(question.question));
            if (formData.HasValue(question.key)) {
                component.SetCompleted();
            }
            else {
                component.SetIncomplete();
            }

            component.onClick = CreateClickEvent(index);
            index++;
        }
    }

    private string CreateQuestionLabel(string questionText) {
        Debug.Log("Hello!!!");
        var patientName = formData.GetValueWithFallback("first_name", "patient");
        return questionText.Replace("{patient}", patientName);
    }

    private void NavigateToQuestion(int index) {
        windowManager.OpenWindow("Form");
        promptController.SetQuestionIndex(index);
    }

    private UnityEvent CreateClickEvent(int questionIndex) {
        var unityEvent = new UnityEvent();
        unityEvent.AddListener(() => NavigateToQuestion(questionIndex));
        return unityEvent;
    }

    private void RemoveChildren() {
        TransformUtilities.GetChildren(transform).ForEach(o => Destroy(o.gameObject));
    }
}