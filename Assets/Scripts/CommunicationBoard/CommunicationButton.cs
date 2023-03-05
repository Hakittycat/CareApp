using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommunicationButton : MonoBehaviour, IPointerClickHandler {
    private AudioClip audioClip;
    private AudioSource source;

    public string GetAudioClipName() {
        return audioClip.name;
    }

    public void SetAudioClip(AudioClip clip) {
        audioClip = clip;
    }

    private void Start() {
        source = FindObjectOfType<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (source != null) {
            source.Stop();
            source.PlayOneShot(audioClip, 0.5f);
        }
    }
}