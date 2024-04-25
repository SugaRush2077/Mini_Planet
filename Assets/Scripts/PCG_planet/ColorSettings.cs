using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static ColorSettings.BiomeColorSettings;

[CreateAssetMenu()]
public class ColorSettings : ScriptableObject
{
    public Material planetMaterial;
    public BiomeColorSettings biomeColorSettings;
    public Gradient oceanColor;
    

    [System.Serializable]
    public class BiomeColorSettings
    {
        public const int NumOfBiome = 5;
        public Biome[] biomes;
        public NoiseSettings noise;
        public float noiseOffset = 0;
        public float noiseStrength = 0;
        [Range(0, 1)]
        public float blendAmount = 0;

        [System.Serializable]
        public class Biome
        {
            public Gradient gradient;
            public Color tint;
            [Range(0, 1)]
            public float startHeight;
            [Range(0, 1)]
            public float tintPercent;

            public Biome()
            {
            }
            public Biome(Gradient gradient, Color tint, float startHeight, float tintPercent)
            {
                this.gradient = gradient;
                this.tint = tint;
                this.startHeight = startHeight;
                this.tintPercent = tintPercent;
            }

            public Color RandomTint()
            {
                Color color = Color.white;
                color.r = Random.value;
                color.g = Random.value;
                color.b = Random.value;

                return color;
            }

            public void initializeBiome()
            {
                this.gradient = new Gradient();
                float f = Random.value;
                if(f < .2f)
                {
                    this.tint = RandomTint();
                    this.tintPercent = Random.Range(.01f, 0.3f);
                }
                else
                {
                    this.tint = Color.white;
                    this.tintPercent = 0;
                }
                this.startHeight = Random.value;
                
            }
        }

        public BiomeColorSettings()
        {
            biomes = new Biome[NumOfBiome];
        }
    }

    static public Color RandomColor()
    {
        Color color = Color.white;
        color.r = Random.value;
        color.g = Random.value;
        color.b = Random.value;

        return color;
    }

    public void changeColorByRandom()
    {
        Debug.Log("Color generate setting: Random");
        Palette palette = RandomPaletteGenerator();
        biomeColorSettings.biomes = new Biome[palette.colorAmount];

        // All Biomes
        BiomeColorGenerator(biomeColorSettings.biomes, palette);
        setRestOfColorNoise();
        OceanGradientGenerator(oceanColor, palette);
    }

    public void changeColorByPalette(Palette desirePalette)
    {
        Debug.Log("Color generate setting: Palette");
        Debug.Log("Selected palette has " + desirePalette.colorArray.Length + " colors!");

        // Biome ---------------------------------------------------------------
        biomeColorSettings.biomes = new Biome[desirePalette.colorAmount];
        // All Biomes
        BiomeColorGenerator(biomeColorSettings.biomes, desirePalette);
        setRestOfColorNoise();
        OceanGradientGenerator(oceanColor, desirePalette);
    }
    
    public void changeColorByEarth()
    {
        Debug.Log("Color generate setting: Earth");
        Palette groundPalette = generateEarthGroundPalette();
        Palette oceanPalette = generateEarthOceanPalette();

        biomeColorSettings.biomes = new Biome[groundPalette.colorAmount];
        BiomeColorGenerator(biomeColorSettings.biomes, groundPalette);
        setRestOfColorNoise();
        OceanGradientGenerator(oceanColor, oceanPalette);
    }

    static void OceanGradientGenerator(Gradient oceanGradient, Palette palette)
    {
        int NumOfOcean = palette.colorAmount;
        if(NumOfOcean > 8)
        {
            NumOfOcean = 8;
        }
        GradientColorKey[] gck = new GradientColorKey[NumOfOcean];
        GradientAlphaKey[] gak = new GradientAlphaKey[NumOfOcean];
        float randomTime = 0;
        for (int i = 0; i < NumOfOcean; i++)
        {
            int rand = Random.Range(0, palette.colorArray.Length);
            gck[i].color = palette.colorArray[rand];
            gck[i].time = Random.Range(randomTime, 1);
            randomTime = gck[i].time;
        }
        oceanGradient.SetKeys(gck, gak);
    }

    static Gradient GradientGenerator(Palette palette)
    {
        const float offset = 0.05f;
        int num = palette.colorAmount;
        if (num > 8)
        {
            num = 8;
        }
        GradientColorKey[] gck = new GradientColorKey[num];
        GradientAlphaKey[] gak = new GradientAlphaKey[num];

        float boundaryValue = 0;
        for (int j = 0; j < num; j++)
        {
            int rand = Random.Range(0, palette.colorArray.Length);
            gck[j].color = palette.colorArray[rand];
            boundaryValue = Random.Range(boundaryValue - offset, boundaryValue + offset);
            if (j == 0)
            {
                boundaryValue = Mathf.Max(0, boundaryValue);
            }
            if (j == num - 1)
            {
                boundaryValue = Mathf.Min(1, boundaryValue);
            }

            gck[j].time = boundaryValue;
            boundaryValue += (1f / (num - 1f));
        }
        Gradient gradient = new Gradient();
        gradient.SetKeys(gck, gak);
        return gradient;
    }

    static Palette RandomPaletteGenerator()
    {
        int palette_num = Random.Range(5, 10);
        Palette palette = new Palette("Random", palette_num);
        for(int i = 0; i < palette.colorAmount; i++)
        {
            palette.colorArray[i] = RandomColor();
        }
        return palette;
    }

    static void BiomeColorGenerator(Biome[] biomes, Palette palette)
    {
        
        int num_Gradient = palette.colorAmount;
        if(num_Gradient > 8)
        {
            num_Gradient = 8;
        }
        for (int i = 0; i < biomes.Length; i++)
        {
            // In Each Biome
            biomes[i] = new Biome();
            biomes[i].initializeBiome();
            biomes[i].gradient = GradientGenerator(palette);
        }
    }
    private Palette generateEarthGroundPalette()
    {
        //Palette palettes = new Palette[2];
        Palette groundPalette = new Palette("EarthGround", 8);
        groundPalette.addColorInToPalette(SimilarColor(new Color(250f / 255f, 249f / 255f, 89f / 255f)));
        groundPalette.addColorInToPalette(SimilarColor(new Color(91f / 255f, 202f / 255f, 43f / 255f)));
        groundPalette.addColorInToPalette(SimilarColor(new Color(29f / 255f, 123f / 255f, 7f / 255f)));
        groundPalette.addColorInToPalette(SimilarColor(new Color(196f / 255f, 123f / 255f, 26f / 255f)));
        groundPalette.addColorInToPalette(SimilarColor(new Color(149f / 255f, 53f / 255f, 8f / 255f)));
        groundPalette.addColorInToPalette(SimilarColor(new Color(130f / 255f, 107f / 255f, 77f / 255f)));
        groundPalette.addColorInToPalette(SimilarColor(new Color(162f / 255f, 162f / 255f, 162f / 255f)));
        groundPalette.addColorInToPalette(SimilarColor(new Color(255f / 255f, 255f / 255f, 255f / 255f)));
        
        return groundPalette;
    }

    private Palette generateEarthOceanPalette()
    {
        Palette oceanPalette = new Palette("EarthOcean", 3);
        oceanPalette.addColorInToPalette(SimilarColor(new Color(12f / 255f, 0f / 255f, 149f / 255f)));
        oceanPalette.addColorInToPalette(SimilarColor(new Color(43f / 255f, 52f / 255f, 237f / 255f)));
        oceanPalette.addColorInToPalette(SimilarColor(new Color(7f / 255f, 191f / 255f, 236f / 255f)));

        return oceanPalette;
    }

    private Color SimilarColor(Color color)
    {
        float threshold = 0.1f;
        float r = color.r;
        float g = color.g;
        float b = color.b;

        r = Random.Range(Mathf.Max(r - threshold, 0f), Mathf.Min(r + threshold, 1f));
        g = Random.Range(Mathf.Max(g - threshold, 0f), Mathf.Min(g + threshold, 1f));
        b = Random.Range(Mathf.Max(b - threshold, 0f), Mathf.Min(b + threshold, 1f));

        return new Color(r, g, b);
    }

    private void setRestOfColorNoise()
    {
        biomeColorSettings.noiseOffset = Random.Range(0, 1);
        biomeColorSettings.noiseStrength = Random.Range(0, 3);
        biomeColorSettings.blendAmount = Random.Range(0, 1);
    }
}
