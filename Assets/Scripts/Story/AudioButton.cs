using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class AudioButton : MonoBehaviour {
    public AudioSource audioSource;
    public Sprite whenPaused;
    public Sprite whenPlaying;

    private void Start() {
        GetComponent<Button>().onClick.AddListener(WhenClicked);
    }

    public void UpdateAudioButtonSprite() {
        var playing = audioSource.isPlaying;
        GetComponent<Image>().sprite = playing ? whenPlaying : whenPaused;
    }

    private void WhenClicked() {
        if (audioSource.isPlaying) {
            audioSource.Pause();
        }
        else {
            audioSource.UnPause();
        }

        UpdateAudioButtonSprite();
    }
}