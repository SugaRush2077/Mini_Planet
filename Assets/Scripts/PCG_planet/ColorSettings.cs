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
                //this.tint = RandomTint();
                this.tint = Color.white;
                this.startHeight = Random.value;
                this.tintPercent = Random.Range(0, 0.5f);
            }
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

        float randomTime = -1;
        for(int i = 0; i < NumOfGradient; i++)
        {
            gck[i].color = RandomColor();
            gck[i].time = Random.Range(randomTime, 1);
            randomTime = gck[i].time;
        }
        
        oceanColor.SetKeys(gck, gak);
    }
    
    public void setColorAsPalette()
    {

    }

    void ShuffleColor(Color[] arr)
    {

    }

    public void changeColorByPalette(Palette desirePalette)
    {
        //Color[] randColorArr = {Color.red, Color.blue, Color.gray, Color.green, Color.yellow};
        
        int NumOfBiome = 2;
        int NumOfOcean = desirePalette.colorArray.Length - NumOfBiome;

        // Biome
        biomeColorSettings.biomes = new Biome[NumOfBiome];
        // All Biomes
        for (int i = 0; i < biomeColorSettings.biomes.Length; i++)
        {
            // In Each Biome
            biomeColorSettings.biomes[i] = new Biome();
            biomeColorSettings.biomes[i].initializeBiome();
            //int NumOfGradient = Random.Range(2, 5);
            GradientColorKey[] biome_gck = new GradientColorKey[1];
            GradientAlphaKey[] biome_gak = new GradientAlphaKey[1];
            float biome_randomTime = -1;
            biome_gck[0].color = desirePalette.colorArray[i];
            biome_gck[0].time = Random.Range(biome_randomTime, 1);
            
            biomeColorSettings.biomes[i].gradient.SetKeys(biome_gck, biome_gak);
        }
        setRestColorNoise();

        // Ocean
        GradientColorKey[] gck = new GradientColorKey[NumOfOcean];
        GradientAlphaKey[] gak = new GradientAlphaKey[NumOfOcean];
        float randomTime = -1;
        for (int i = 0; i < NumOfOcean; i++)
        {
            gck[i].color = desirePalette.colorArray[i + 2];
            gck[i].time = Random.Range(randomTime, 1);
            randomTime = gck[i].time;
        }
        oceanColor.SetKeys(gck, gak);
    }
    
    /*
    public void setPaletteArray()
    {
        
        palettesArray = new Palette[1];

        Palette palette = new Palette(name, 5);
        palette.initializePalette();

        palette.addColorIntoPalette(new HexColor("264653"));
        palette.addColorIntoPalette(new HexColor("2a9d8f"));
        palette.addColorIntoPalette(new HexColor("e9c46a"));
        palette.addColorIntoPalette(new HexColor("f4a261"));
        palette.addColorIntoPalette(new HexColor("e76f51"));
        palettesArray[0] = palette;

    }*/
    /*
    public void useColorPalette()
    {
        Debug.Log("Color generate: Palette");

        setPaletteArray();
        int numOfColor = palettesArray[0].colorAmount;
        biomeColorSettings.biomes = new Biome[numOfColor];

        for(int i = 0; i < numOfColor;i++)
        {
            biomeColorSettings.biomes[i] = new Biome();
            biomeColorSettings.biomes[i].initializeBiome();

            GradientColorKey[] gck = new GradientColorKey[1];
            GradientAlphaKey[] gak = new GradientAlphaKey[1];

            //float randomTime = -1;
            
            gck[0].color = palettesArray[0].colorArray[i].myColor;
            //gck[i].time = Random.Range(randomTime, 1);
            //randomTime = gck[i].time;
            
            biomeColorSettings.biomes[i].gradient.SetKeys(gck, gak);
        }
        setRestColorNoise();
    }*/

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

            float randomTime = -1;
            for (int j = 0; j < NumOfGradient; j++)
            {
                gck[j].color = RandomColor();
                gck[j].time = Random.Range(randomTime, 1);
                randomTime = gck[j].time;
            }
            biomeColorSettings.biomes[i].gradient.SetKeys(gck, gak);
        }
        setRestColorNoise();


    }

    private void setRestColorNoise()
    {
        biomeColorSettings.noiseOffset = Random.Range(0, 1);
        biomeColorSettings.noiseStrength = Random.Range(0, 3);
        biomeColorSettings.blendAmount = Random.Range(0, 1);
    }
}
