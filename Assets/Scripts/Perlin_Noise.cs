
using UnityEngine;

public class Perlin_Noise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offset_X = 0;
    public float offset_Y = 0;

    private void Start()
    {
        offset_X = Random.Range(0f, 99999f);
        offset_Y = Random.Range(0f, 99999f);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    private void Update()
    {
        
    }

    Texture2D GenerateTexture() 
    { 
        Texture2D texture = new Texture2D(width, height);

        // Generate Perlin Noise
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++) 
            { 
                Color myColor = CalculateColor(x, y);
                texture.SetPixel(x, y, myColor);
            
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoor = (float)x / width * scale + offset_X;
        float yCoor = (float)y / height * scale + offset_Y;


        float sample = Mathf.PerlinNoise(xCoor, yCoor);
        return new Color(sample, sample, sample);

    }
}
