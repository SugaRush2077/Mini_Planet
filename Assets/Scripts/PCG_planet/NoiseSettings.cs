using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum FilterType { Simple, Ridgid };
    public FilterType filterType;

    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RidgidNoiseSettings ridgidNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float strength = 1;
        [Range(1, 8)]
        public int numLayers = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float persistence = .5f;

        public Vector3 center = new Vector3(0, 0, 0);
        public float minValue = 0;

        public SimpleNoiseSettings() {}
        public SimpleNoiseSettings(float strength, int numLayers, float baseRoughness, float roughness, float persistence, Vector3 center, float minValue)
        {
            this.strength = strength;
            this.numLayers = numLayers;
            this.baseRoughness = baseRoughness;
            this.roughness = roughness;
            this.persistence = persistence;
            this.center = center;
            this.minValue = minValue;
        }
    }

    [System.Serializable]
    public class RidgidNoiseSettings
    {
        public float strength = 1;
        [Range(1, 8)]
        public int numLayers = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float persistence = .5f;

        public Vector3 center = new Vector3(0,0,0);
        public float minValue = 0;
        public float weightMultiplier = .8f;

        public RidgidNoiseSettings() {}
        public RidgidNoiseSettings(float strength, int numLayers, float baseRoughness, float roughness, float persistence, Vector3 center, float minValue, float weightMultiplier)
        {
            this.strength = strength;
            this.numLayers = numLayers;
            this.baseRoughness = baseRoughness;
            this.roughness = roughness;
            this.persistence = persistence;
            this.center = center;
            this.minValue = minValue;
            this.weightMultiplier = weightMultiplier;
        }
        
    }

    public NoiseSettings() {
        simpleNoiseSettings = new SimpleNoiseSettings();
        ridgidNoiseSettings = new RidgidNoiseSettings();
        filterType = new FilterType();
    }

    public void RandomizeSettings()
    {
        bool usingDefault = false;
        if (usingDefault)
        {
            // default   
            if (filterType == FilterType.Simple)
            {
                simpleNoiseSettings.strength = Random.Range(.01f, .15f);
                simpleNoiseSettings.numLayers = 3;
                simpleNoiseSettings.baseRoughness = Random.Range(2f, 3f);
                simpleNoiseSettings.roughness = Random.Range(2f, 4f);
                simpleNoiseSettings.persistence = Random.Range(.1f, .5f);
                simpleNoiseSettings.center = new Vector3(0, 0, 0);
                simpleNoiseSettings.minValue = Random.Range(.5f, 1f);
                //Debug.Log("RandomSimple");
            }
            else
            {
                ridgidNoiseSettings.strength = Random.Range(.2f, 1f);
                ridgidNoiseSettings.numLayers = 4;
                ridgidNoiseSettings.baseRoughness = Random.Range(.2f, 3f);
                ridgidNoiseSettings.roughness = Random.Range(.5f, 3f);
                ridgidNoiseSettings.persistence = Random.Range(0, 1f);
                ridgidNoiseSettings.center = new Vector3(5, 5, 5);
                ridgidNoiseSettings.minValue = Random.Range(0, .5f);
                ridgidNoiseSettings.weightMultiplier = Random.Range(.5f, .8f);
                //Debug.Log("RandomRidgid");
            }
        }
        else // Personalize
        {
            if (filterType == FilterType.Simple)
            {
                simpleNoiseSettings.strength = Random.Range(.01f, .4f);
                simpleNoiseSettings.numLayers = 4;
                simpleNoiseSettings.baseRoughness = Random.Range(1f, 3f);
                simpleNoiseSettings.roughness = Random.Range(1f, 4f);
                simpleNoiseSettings.persistence = Random.Range(.1f, .5f);
                simpleNoiseSettings.center = new Vector3(0, 0, 0);
                simpleNoiseSettings.minValue = Random.Range(.1f, 1.5f);
                //Debug.Log("RandomSimple");
            }
            else
            {
                ridgidNoiseSettings.strength = Random.Range(.2f, 1f);
                ridgidNoiseSettings.numLayers = 4;
                ridgidNoiseSettings.baseRoughness = Random.Range(.2f, 3f);
                ridgidNoiseSettings.roughness = Random.Range(.5f, 3f);
                ridgidNoiseSettings.persistence = Random.Range(0, 1f);
                ridgidNoiseSettings.center = new Vector3(5, 5, 5);
                ridgidNoiseSettings.minValue = Random.Range(0, .5f);
                ridgidNoiseSettings.weightMultiplier = Random.Range(.5f, .8f);
                //Debug.Log("RandomRidgid");
            }
        }
        
    }
}
