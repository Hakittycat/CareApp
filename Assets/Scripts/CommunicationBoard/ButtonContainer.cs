using Newtonsoft.Json.Linq;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContainer : MonoBehaviour {
    private string category;
    public List<GameObject> buttons = new List<GameObject>();
    public GameObject buttonPrefab;
    public GameObject boardManager;

    private void Start() {
        LoadButtons();
    }

    public void SetCategory(string communicationCategory) {
        category = communicationCategory;
        LoadButtons();
    }

    public IEnumerator LoadButtons() {
        ClearButtons();
        var categoryDirectory = $"Communication Board/{category}/";
        var sprites = Resources.LoadAll<Sprite>(categoryDirectory);
        var audioClips = Resources.LoadAll<AudioClip>(categoryDirectory);
        foreach (var sprite in sprites) {
            var clip = FindByName(sprite.name, audioClips);
            var createdButton = CreateButton(sprite, clip);
            buttons.Add(createdButton);
        }
        yield return GetPhotos();
    }

    public IEnumerator GetPhotos()
    {
        JObject body = new JObject {
            { "username", WebRequestUtilities.username },
            { "photoType", category }
        };

        var request = WebRequestUtilities.CreatePostRequest($"{WebRequestUtilities.URL}/getphotos", body);
        yield return request.SendWebRequest();

        if (request.IsSuccess())
        {
            JObject response = JObject.Parse(request.downloadHandler.text);
            JArray results = response["photos"]?.Value<JArray>() ?? new JArray();
            foreach (var result in results)
            {
                string photoType = result["photoType"]?.Value<string>();
                string filename = result["filename"]?.Value<string>();
                string description = result["description"]?.Value<string>();
                string base64 = result["base64"]?.Value<string>();

                byte[] bytes = Convert.FromBase64String(base64);
                
                Texture2D t = new Texture2D(355, 355, TextureFormat.RGB24, false);
                t.LoadImage(bytes);
                Sprite sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(117.50f, 117.50f));
                
                var createdButton = CreateButton(sprite, null);

                buttons.Add(createdButton);

                var parent = createdButton.transform.parent.gameObject;

                GameObject obj = new GameObject($"{description}_label");
                var text = obj.AddComponent<TextMeshProUGUI>();
                text.text = description;
                text.fontSize = 38;
                text.alignment = TextAlignmentOptions.Center;
                text.outlineColor = Color.black;
                text.outlineWidth = 10;
                text.fontStyle = FontStyles.Bold;

                obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -84, 0);
                Instantiate(obj, createdButton.transform);

            }

        }
    }

    private GameObject CreateButton(Sprite sprite, AudioClip? clip) {
        var createdButton = Instantiate(buttonPrefab, transform);
        var image = createdButton.GetComponent<Image>();
        var component = createdButton.GetComponent<CommunicationButton>();
        image.sprite = sprite;
        if (clip != null) component.SetAudioClip(clip);
        var logger = createdButton.AddComponent<ClickLogger>();
        logger.data = sprite.name;
        return createdButton;
    }

    private static AudioClip FindByName(string clipName, IEnumerable<AudioClip> clips) {
        return clips.FirstOrDefault(audioClip => audioClip.name.Equals(clipName));
    }

    private void ClearButtons() {
        foreach (var button in buttons) {
            Destroy(button);
        }

        buttons.Clear();
    }

}