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
                    this.tintPercent = Random.Range(.01f, 0.5f);
                }
                else
                {
                    this.tint = Color.white;
                    this.tintPercent = 0;
                }
                this.startHeight = Random.value;
                
            }
            /*
            public void NoTint()
            {
                initializeBiome();
                this
            }*/
        }

        public BiomeColorSettings()
        {
            biomes = new Biome[NumOfBiome];
        }
    }

    public Color RandomColor()
    {
        Color color = Color.white;
        color.r = Random.value;
        color.g = Random.value;
        color.b = Random.value;

        return color;
    }

    public void changeOceanColor()
    {
        int NumOfGradient = Random.Range(2, 5); 
        
        GradientColorKey[] gck = new GradientColorKey[NumOfGradient];
        GradientAlphaKey[] gak = new GradientAlphaKey[NumOfGradient];

        float randomTime = 0;
        for(int i = 0; i < NumOfGradient; i++)
        {
            gck[i].color = RandomColor();
            gck[i].time = Random.Range(randomTime, 1);
            randomTime = gck[i].time;
        }
        
        oceanColor.SetKeys(gck, gak);
    }
    
    public void changeColorByPalette(Palette desirePalette)
    {
        //Color[] randColorArr = {Color.red, Color.blue, Color.gray, Color.green, Color.yellow};
        int[] order = new int[desirePalette.colorAmount];
        Helper.initializeAndShuffle(order);
        int n = (desirePalette.colorArray.Length / 2);
        int NumOfBiome = Random.Range(n-1, n+1);
        int NumOfOcean = desirePalette.colorArray.Length - NumOfBiome;
        Debug.Log("Selected palette has " + desirePalette.colorArray.Length + " colors!");

        // Biome ---------------------------------------------------------------
        biomeColorSettings.biomes = new Biome[NumOfBiome];
        // All Biomes
        for (int i = 0; i < biomeColorSettings.biomes.Length; i++)
        {
            // In Each Biome
            biomeColorSettings.biomes[i] = new Biome();
            biomeColorSettings.biomes[i].initializeBiome();
            int NumOfGradient = 5; // Set Gradient for just 1 color!
            GradientColorKey[] biome_gck = new GradientColorKey[NumOfGradient];
            GradientAlphaKey[] biome_gak = new GradientAlphaKey[NumOfGradient];

            float biome_randomTime = 0;
            for (int j = 0; j < NumOfGradient; j++)
            {
                biome_gck[j].color = desirePalette.colorArray[order[i]];
                biome_gck[j].time = Random.Range(biome_randomTime, 1);
                biome_randomTime = biome_gck[j].time;
            }
            biomeColorSettings.biomes[i].gradient.SetKeys(biome_gck, biome_gak);
        }
        setRestOfColorNoise();

        // Ocean ---------------------------------------------------------------
        GradientColorKey[] gck = new GradientColorKey[NumOfOcean];
        GradientAlphaKey[] gak = new GradientAlphaKey[NumOfOcean];
        float randomTime = 0;
        for (int i = 0; i < NumOfOcean; i++)
        {
            gck[i].color = desirePalette.colorArray[order[i + NumOfBiome]];
            gck[i].time = Random.Range(randomTime, 1);
            randomTime = gck[i].time;
        }
        oceanColor.SetKeys(gck, gak);
    }
    
    public void changeBiomeColor()
    {
        Debug.Log("Color generate: Random");
        int num = Random.Range(2, NumOfBiome);
        biomeColorSettings.biomes = new Biome[num];
        
        // All Biomes
        for(int i = 0; i < biomeColorSettings.biomes.Length; i++)
        {
            // In Each Biome
            biomeColorSettings.biomes[i] = new Biome();
            biomeColorSettings.biomes[i].initializeBiome();
            int NumOfGradient = Random.Range(2, 5);
            GradientColorKey[] gck = new GradientColorKey[NumOfGradient];
            GradientAlphaKey[] gak = new GradientAlphaKey[NumOfGradient];

            float randomTime = 0;
            for (int j = 0; j < NumOfGradient; j++)
            {
                gck[j].color = RandomColor();
                gck[j].time = Random.Range(randomTime, 1);
                randomTime = gck[j].time;
            }
            biomeColorSettings.biomes[i].gradient.SetKeys(gck, gak);
        }
        setRestOfColorNoise();
    }

    public void earthBiomeColor()
    {
        int num = 3;
        biomeColorSettings.biomes = new Biome[num];

        // All Biomes
        for (int i = 0; i < biomeColorSettings.biomes.Length; i++)
        {
            // In Each Biome
            biomeColorSettings.biomes[i] = new Biome();
            biomeColorSettings.biomes[i].initializeBiome();
            int NumOfGradient = 8;
            GradientColorKey[] gck = new GradientColorKey[NumOfGradient];
            GradientAlphaKey[] gak = new GradientAlphaKey[NumOfGradient];

            gck[0].color = new Color(250f / 255f, 249f / 255f, 89f / 255f);
            gck[0].time = 0f;
            gck[1].color = new Color(91f / 255f, 202f / 255f, 43f / 255f);
            gck[1].time = .04f;
            gck[2].color = new Color(29f / 255f, 123f / 255f, 7f / 255f);
            gck[2].time = .33f;
            gck[3].color = new Color(196f / 255f, 123f / 255f, 26f / 255f);
            gck[3].time = .4f;
            gck[4].color = new Color(149f / 255f, 53f / 255f, 8f / 255f);
            gck[4].time = .6f;
            gck[5].color = new Color(130f / 255f, 107f / 255f, 77f / 255f);
            gck[5].time = .65f;
            gck[6].color = new Color(162f / 255f, 162f / 255f, 162f / 255f);
            gck[6].time = .8f;
            gck[7].color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
            gck[7].time = 1f;

            biomeColorSettings.biomes[i].gradient.SetKeys(gck, gak);
        }
        setRestOfColorNoise();
    }

    public void earthOceanColor()
    {
        int NumOfGradient = 3;

        GradientColorKey[] gck = new GradientColorKey[NumOfGradient];
        GradientAlphaKey[] gak = new GradientAlphaKey[NumOfGradient];

        gck[0].color = new Color(12f / 255f, 0f / 255f, 149f / 255f);
        gck[0].time = 0f;
        gck[1].color = new Color(43f / 255f, 52f / 255f, 237f / 255f);
        gck[1].time = .5f;
        gck[2].color = new Color(7f / 255f, 191f / 255f, 236f / 255f);
        gck[2].time = 1f;

        oceanColor.SetKeys(gck, gak);
    }

        private void setRestOfColorNoise()
    {
        biomeColorSettings.noiseOffset = Random.Range(0, 1);
        biomeColorSettings.noiseStrength = Random.Range(0, 3);
        biomeColorSettings.blendAmount = Random.Range(0, 1);
    }
}
