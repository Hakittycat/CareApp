using Newtonsoft.Json.Linq;
using System.IO;
using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Kakera;

public class CommunicationBoardManager : MonoBehaviour {
    public ButtonContainer buttonContainer;
    public GameObject buttonsPage;
    public GameObject categoriesContainer;
    public TMP_InputField descriptionBox;
    public Button submitButton;
    private string category;
    private string filename;
    private string base64;
    [SerializeField]
    private Unimgpicker imagePicker;

    public void OnPressShowPicker()
    {
        // With v1.1 or greater, you can set the maximum size of the image
        // to save the memory usage.
        string filename = "filename.jpg";
        imagePicker.Show("Select Image", filename);
    }
    private void Start()
    {
        ShowCategories();
        Button btn = submitButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClickSumbit);
        //imagePicker.Completed += SetPhotoData;
        imagePicker.Completed += (string path) =>
        {
            StartCoroutine(LoadImage(path));
        };
    }

    private IEnumerator LoadImage(string path)
    {
        var url = "file://" + path;
        var www = new WWW(url);
        yield return www;

        var texture = www.texture;
        if (texture == null)
        {
            Debug.LogError("Failed to load texture url:" + url);
        }

        //byte[] bytes = File.ReadAllBytes(url);
        //base64 = Convert.ToBase64String(bytes);
        filename = www.url;
        base64 = Convert.ToBase64String(www.texture.EncodeToJPG());
    }

    private void ShowButtonContainer()
    {
        categoriesContainer.SetActive(false);
        buttonsPage.SetActive(true);
        
    }

    private void ShowCategories()
    {
        buttonsPage.SetActive(false);
        categoriesContainer.SetActive(true);
    }

    public void HandleCategoryClick(string categoryName)
    {
        category = categoryName;
        buttonContainer.SetCategory(categoryName);
        ShowButtonContainer();
        StartCoroutine(buttonContainer.LoadButtons());
    }

    public void GoToCategories() {
        ShowCategories();
    }

    public void ChoosePhoto()
    {
        //string[] allowedTypes = { "public.jpg", "public.png" };
        //NativeFilePicker.PickFile(SetPhotoData, allowedTypes);
        OnPressShowPicker();
    }

    private void SetPhotoData(string path)
    {
        Debug.Log(category);
        Debug.Log(path);
        filename = Path.GetFileName(path);
        byte[] bytes = File.ReadAllBytes(path);
        base64 = Convert.ToBase64String(bytes);
    }
    public void OnClickSumbit()
    {
        StartCoroutine(UploadPhoto());

    }
    public IEnumerator UploadPhoto()
    {
        JObject body = new JObject {
            { "username", WebRequestUtilities.username },
            { "photoType", category },
            { "filename", filename },
            { "description", descriptionBox.text },
            { "base64", base64 }
        };

        var request = WebRequestUtilities.CreatePostRequest($"{WebRequestUtilities.URL}/uploadphoto", body);
        yield return request.SendWebRequest();

        if (request.IsSuccess())
        {
            Debug.Log("Photo upload worked!");
        }
        else
        {
            Debug.Log("Photo upload failed :(");
        }
        StartCoroutine(buttonContainer.LoadButtons());
    }
}