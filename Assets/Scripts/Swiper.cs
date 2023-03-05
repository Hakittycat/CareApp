using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * For some reason we need to implement IDragHandler in order for our OnEndDrag method to be called.
 */
public class Swiper : MonoBehaviour, IDragHandler, IEndDragHandler {

    [Range(0, 1)]
    public float dragThreshold;
    public UnityEvent onRightToLeftSwipe;
    public UnityEvent onLeftToRightSwipe;
    
    public void OnEndDrag(PointerEventData eventData) {
        var press = eventData.pressPosition;
        var release = eventData.position;
        var distance = press.x - release.x;
        var screenPercentage = distance / Screen.width;
        if (Math.Abs(screenPercentage) >= dragThreshold) {
            if (screenPercentage < 0) {
                onLeftToRightSwipe.Invoke();
            } else {
                onRightToLeftSwipe.Invoke();
            }
        }
    }
    
    public void OnDrag(PointerEventData eventData) { }
}
