using UnityEngine;
using UnityEngine.UI;

public class PaintableTexture : MonoBehaviour {
    public Image image;
    public int width;
    public int height;
    public Material canvasMaterial;
    public Color defaultColor;
    private Texture2D paintableTexture;

    private void Start() {
        paintableTexture = new Texture2D(width, height);
        canvasMaterial.mainTexture = paintableTexture;
        image.material = canvasMaterial;
        Fill(defaultColor);
    }

    public void Clear() {
        Fill(defaultColor);
    }

    private void Fill(Color color) {
        var pixels = paintableTexture.GetPixels();
        for (var i = 0; i < pixels.Length; i++) {
            pixels[i] = color;
        }

        paintableTexture.SetPixels(pixels);
        UpdateTexture();
    }

    public void UpdateTexture() {
        paintableTexture.Apply(false);
    }

    public void DrawCircle(Vector2 position, int radius, Color color) {
        DrawCircle((int) position.x, (int) position.y, radius, color);
    }

    private void DrawCircle(int cx, int cy, int radius, Color color) {
        for (var x = 0; x <= radius; x++) {
            var d = (int) Mathf.Ceil(Mathf.Sqrt(radius * radius - x * x));
            for (var y = 0; y <= d; y++) {
                var px = cx + x;
                var nx = cx - x;
                var py = cy + y;
                var ny = cy - y;

                paintableTexture.SetPixel(px, py, color);
                paintableTexture.SetPixel(nx, py, color);

                paintableTexture.SetPixel(px, ny, color);
                paintableTexture.SetPixel(nx, ny, color);
            }
        }
    }
}