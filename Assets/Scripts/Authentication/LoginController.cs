using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ButtonManagerBasic))]
public class LoginController : MonoBehaviour {

    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public Authenticator authenticator;

    private void Start() {
        GetComponent<ButtonManagerBasic>().clickEvent.AddListener(OnClickLogin);
    }

    private void OnClickLogin() {
        authenticator.Authenticate(usernameField.text, passwordField.text);
    }
}