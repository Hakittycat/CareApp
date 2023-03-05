using System.Collections.Generic;
using UnityEngine;

public static class TransformUtilities {
    public static List<Transform> GetChildren(Transform parent) {
        var children = new List<Transform>();
        for (var i = 0; i < parent.childCount; i++) {
            children.Add(parent.GetChild(i));
        }

        return children;
    }
}