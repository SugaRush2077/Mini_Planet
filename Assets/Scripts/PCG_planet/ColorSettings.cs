using System.Collections;
using System.Collections.Generic;
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
        const int NumOfBiome = 3;
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
                /*
                this.gradient = new Gradient();
                this.tint = Random.ColorHSV();
                this.startHeight = Random.value;
                this.tintPercent = Random.value;*/
            }
            public Biome(Gradient gradient, Color tint, float startHeight, float tintPercent)
            {
                this.gradient = gradient;
                this.tint = tint;
                this.startHeight = startHeight;
                this.tintPercent = tintPercent;
            }
        }

        public BiomeColorSettings()
        {
            biomes = new Biome[NumOfBiome];
        }


    }

    public ColorSettings() 
    {
    
    
    }
    /*
    public void GenerateColorSetting()
    {
        //int num = Random.Range(1, NumOfBiome);
        biomeColorSettings.biomes = new Biome[num];
        
        int NumOfColorsInBiome = 2;
        float gradientCount = .2f;
        // All Biomes
        for(int i = 0; i < biomeColorSettings.biomes.Length; i++)
        {
            var colors = new GradientColorKey[num];
            var alphas = new GradientAlphaKey[num];
            Color RandomTintColor = Random.ColorHSV();
            
            biomeColorSettings.biomes[i].startHeight = Random.Range(0, 1);
            biomeColorSettings.biomes[i].tintPercent = Random.Range(0, 1);
            biomeColorSettings.biomes[i].tint = RandomTintColor;
            for(int j = 0; j < NumOfColorsInBiome; j++)
            {
                Color RandomBiomeColor = Random.ColorHSV();
                if (gradientCount > 1)
                    gradientCount = 1;
                colors[i] = new GradientColorKey(RandomBiomeColor, gradientCount);
                alphas[i] = new GradientAlphaKey(1f, 0f);
                gradientCount += .2f;
            }
            biomeColorSettings.biomes[i].gradient.SetKeys(colors, alphas);
        }
        biomeColorSettings.noiseOffset = Random.Range(0, 1);
        biomeColorSettings.noiseStrength = Random.Range(0, 3);
        biomeColorSettings.blendAmount = Random.Range(0, 1);


    }*/
}
