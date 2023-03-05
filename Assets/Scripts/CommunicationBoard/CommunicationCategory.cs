using UnityEngine;
using UnityEngine.EventSystems;

public class CommunicationCategory : MonoBehaviour, IPointerClickHandler {
    public string category;
    public CommunicationBoardManager manager;

    public void OnPointerClick(PointerEventData eventData) {
        manager.HandleCategoryClick(category);
    }
}