using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class Utilities {
    public static string JoinStrings(IReadOnlyList<string> list) {
        var builder = new StringBuilder();
        for (var i = 0; i < list.Count; i++) {
            var value = list[i];
            builder.Append(value);
            if (i < list.Count - 1) {
                builder.Append(",");
            }
        }

        return builder.ToString();
    }

    public static void SetImageAlpha(Image image, float alpha) {
        var color = image.color;
        color.a = alpha;
        image.color = color;
    }

}