using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableCard : MonoBehaviour, IPointerClickHandler {

    public UnityEvent unityEvent;
    
    public void OnPointerClick(PointerEventData eventData) {
        unityEvent.Invoke();
    }
}
