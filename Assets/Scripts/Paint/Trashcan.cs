using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trashcan : MonoBehaviour, IPointerClickHandler {
    public PaintableTexture texture;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        texture.Clear();
        animator.Play("wiggle");
    }
}