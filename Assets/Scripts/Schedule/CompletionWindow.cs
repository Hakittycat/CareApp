using UnityEngine;

public class CompletionWindow : MonoBehaviour {

    public ParticleSystem particles;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlayEffects() {
        particles.Play();
        audioSource.PlayOneShot(audioClip, 1.0f);
    }
}