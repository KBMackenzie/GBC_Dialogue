using UnityEngine;

namespace GBC_Dialogue.Helpers;

internal static class LoadTexture
{
    public static Sprite SpriteFromBytes(byte[] array)
    {
        Texture2D tex = new Texture2D(1, 1);
        ImageConversion.LoadImage(tex, array);
        tex.filterMode = FilterMode.Point; // Pixel-perfect filter.
        Rect texRect = new Rect(0, 0, tex.width, tex.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        return Sprite.Create(tex, texRect, pivot);
    }
}
