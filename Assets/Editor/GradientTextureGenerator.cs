using UnityEngine;
using UnityEditor;
using System.IO;

public class SimpleGradientGen : EditorWindow
{
    [MenuItem("Tools/Create Gradient Mask")]
    static void Create()
    {
        int width = 256;
        int height = 4;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        for (int x = 0; x < width; x++)
        {
            // izquierda = blanco (1), derecha = negro (0)
            float value = 1f - ((float)x / (width - 1));
            Color c = new Color(value, value, value);
            for (int y = 0; y < height; y++)
                tex.SetPixel(x, y, c);
        }

        tex.Apply();
        string path = "Assets/Textures/GradientMask.png";
        Directory.CreateDirectory("Assets/Textures");
        File.WriteAllBytes(path, tex.EncodeToPNG());
        AssetDatabase.Refresh();
        Debug.Log("✅ Gradiente creado en: " + path);
    }
}