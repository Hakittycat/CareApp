using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PromptController : MonoBehaviour {
    public FormManager formManager;
    public WindowManager windowManager;
    public FormPromptResources resources;
    private PersistentFormData data;
    public QuestionnaireData questionnaireData;
    private QuestionnaireSection current;
    public UnityEvent onClickComplete;
    private int questionIndex;

    private void Awake() {
        data = FindObjectOfType<PersistentFormData>();
    }

    public QuestionnaireSection GetActiveSection() {
        return current;
    }
    
    private void InsertPlaceholder() {
        var patientName = data.GetValueWithFallback("first_name", "patient");
        Debug.Log("Insert Placeholder");
        Debug.Log("Name here!!!!: " + patientName);
        resources.description.text = resources.description.text.Replace("{patient}", patientName);
        resources.question.text = resources.question.text.Replace("{patient}", patientName);
    }

    /**
     * If at the last question, update form value then go to sections window;
     * If NOT at the last question, update form value, increment question index, and update view.
     */
    public void OnNextButtonClick() {
        UpdateFormValue();
        if (isAtLastQuestion()) {
            windowManager.OpenWindow("Sections");
            onClickComplete.Invoke();
        }
        else {
            questionIndex++;
            SetView();
        }
    } 

    public void OnBackButtonClick() {
        if (questionIndex <= 0) return;
        questionIndex--;
        SetView();
    }

    /**
     * Steps:
     * 1. Reset the question index.
     * 2. Look up the questionnaire section from the list given the question name.
     * 3. Update the 'current' section to the newly found section.
     * 4. Invoke the SetView() method to update the page to the first question of the section.
     */
    public void SetSection(string sectionName) {
        questionIndex = 0;
        var section = questionnaireData.FindSection(sectionName);
        if (section == null) return;
        current = section;
        SetView();
    }

    private void UpdateProgressbar() {
        var keys = current.GetQuestionKeys();
        var completed = data.CountCompleted(keys);
        resources.SetProgress(completed, keys.Count);
    }

    /**
     * Steps:
     * 1. Clear all of the old inputs.
     * 2. Set input value if one exists in the FormData object.
     * 3. Get the current question from the section using the question index.
     * 4. Set the question progress, name, and description of the question.
     * 5. Show the needed input element for the question.
     */
    private void SetView() {
        resources.ClearInputs();
        var details = current.GetQuestionAtPosition(questionIndex);
        UpdateProgressbar();
        resources.SetQuestion(details.question);
        resources.SetDescription(details.description);
        resources.ShowInputElement(details.input);
        if (data.HasValue(details.key)) {
            resources.SetAnswer(data.GetValue(details.key));
        }

        UpdateButtonStatus();
        UpdateContinueButtonText();
        InsertPlaceholder();
    }

    public void SetQuestionIndex(int index) {
        questionIndex = index;
        SetView();
    }

    private void UpdateButtonStatus() {
        resources.backButton.GetComponent<Button>().enabled = questionIndex != 0;
        var image = resources.backButton.GetComponent<Image>();
        var alpha = questionIndex == 0 ? 0.25f : 1f;
        Utilities.SetImageAlpha(image, alpha);
    }

    private void UpdateFormValue() {
        var details = current.GetQuestionAtPosition(questionIndex);
        var input = details.input.GetComponent<InputElement>().GetInputValue();
        data.SetData(details.key, input);
        formManager.UpdateAnswer(details.key, input);
    }

    private void UpdateContinueButtonText() {
        resources.nextButton.normalText.text = isAtLastQuestion() ? "COMPLETE" : "NEXT";
    }

    private bool isAtLastQuestion() {
        return questionIndex == current.questions.Count - 1;
    }
}