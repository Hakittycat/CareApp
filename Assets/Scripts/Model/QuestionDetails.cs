using System;
using UnityEngine;

[Serializable]
public class QuestionDetails {
    public string question;
    public string description;
    [InspectorName("The game object used for user input for this question.")]
    public GameObject input;
    public string key;
}