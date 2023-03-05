using System;
using UnityEngine;
using UnityEngine.UI;

public class RadioButton : MonoBehaviour {
    
    public RadioButtonImages images;
    public RadioButtonColors colors;

    public void SetSelected() {
        images.image.color = colors.selected;
        images.glow.color = colors.selected;
    }

    public void SetUnselected() {
        images.image.color = colors.unselected;
        images.glow.color = colors.unselected;
    }

    [Serializable]
    public class RadioButtonImages {
        public Image image;
        public Image glow;
    }

    [Serializable]
    public class RadioButtonColors {
        public Color selected;
        public Color unselected;
    }
}