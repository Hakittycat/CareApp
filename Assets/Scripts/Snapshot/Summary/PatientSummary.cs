using UnityEngine;

public class PatientSummary : MonoBehaviour {
    private PersistentFormData formData;
    private QuestionnaireData questionnaireData;
    public GameObject rowPrefab;

    private void Awake() {
        formData = FindObjectOfType<PersistentFormData>();
        questionnaireData = FindObjectOfType<QuestionnaireData>();
        Validator.CheckNotNull(formData, "FormData missing!");
        Validator.CheckNotNull(questionnaireData, "Questionnaire missing!");
    }

    private void Start() {
        Populate();
    }

    private void Populate() {
        foreach (var question in questionnaireData.GetAllQuestions()) {
            var row = Instantiate(rowPrefab, transform);
            var component = row.GetComponent<SummaryRow>();
            component.question.text = question.question;
            component.answer.text = formData.GetValueWithFallback(question.key, "Unanswered");
        }
    }
}