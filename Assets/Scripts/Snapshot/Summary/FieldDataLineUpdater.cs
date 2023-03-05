using UnityEngine;

public class FieldDataLineUpdater : MonoBehaviour {
    public void UpdateDataLines() {
        var dataLines = GetComponentsInChildren<FieldDataLine>();
        foreach (var fieldDataLine in dataLines) {
            fieldDataLine.UpdateField();
        }
    }
}