using UnityEngine;

public class RegistrationCompleteHandler: MonoBehaviour{

    public LoginController loginController;
    public RegistrationController registrationController;

    public void HandleSuccessfulRegistration() {
        loginController.usernameField.text = registrationController.GetGeneratedUsername();
    }
}