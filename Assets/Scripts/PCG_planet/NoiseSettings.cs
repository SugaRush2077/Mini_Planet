using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

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

    Vector3 randomVect3()
    {
        Vector3 vtr = new Vector3();
        vtr.x = Random.Range(-360, 360);
        vtr.y = Random.Range(-360, 360);
        vtr.z = Random.Range(-360, 360);
        return vtr;
    }

    void setParameter()
    {

        float rand_strength = Random.Range(.1f, 1f);
        float rand_baseRough;
        float rand_minValue;

        // Use strength to control rest of parameters
        
        if(rand_strength < 0.2f)
        {
            rand_baseRough = Random.Range(1.5f, 5f);
            rand_minValue = Random.Range(rand_strength + .1f, .6f);
        }
        else if (rand_strength < 0.4f)
        {
            rand_baseRough = Random.Range(1.5f, 6f);
            rand_minValue = Random.Range(.5f, .8f);
        }
        else if (rand_strength < 0.6f)
        {
            rand_baseRough = Random.Range(1.5f, 4f);
            rand_minValue = Random.Range(.45f, .8f);
        }
        else if (rand_strength < 0.8f)
        {
            rand_baseRough = Random.Range(2f, 2.5f);
            rand_minValue = Random.Range(.5f, .8f);
        }
        else // 2 <= rand_strength <= 3
        {
            rand_baseRough = Random.Range(1f, 2f);
            rand_minValue = Random.Range(.6f, .85f);
        }

        simpleNoiseSettings.strength = rand_strength;
        simpleNoiseSettings.baseRoughness = rand_baseRough;
        simpleNoiseSettings.minValue = rand_minValue;
    }



    public void RandomizeSettings(string type)
    {
        //bool usingDefault = true;
        if (type == "Random")
        {
            // default   
            if (filterType == FilterType.Simple)
            {
                //simpleNoiseSettings.strength = Random.Range(.1f, .5f);
                simpleNoiseSettings.numLayers = 3;
                //simpleNoiseSettings.baseRoughness = Random.Range(.7f, 5f);
                simpleNoiseSettings.roughness = Random.Range(.01f, 3f);
                simpleNoiseSettings.persistence = Random.Range(-.2f, .2f);
                simpleNoiseSettings.center = randomVect3();
                //simpleNoiseSettings.minValue = Random.Range(.6f, .9f);
                //Debug.Log("RandomSimple");
                setParameter();
            }
            else
            {
                ridgidNoiseSettings.strength = Random.Range(.1f, .3f);
                ridgidNoiseSettings.numLayers = 4;
                ridgidNoiseSettings.baseRoughness = Random.Range(1f, 2f);
                ridgidNoiseSettings.roughness = Random.Range(.01f, 2f);
                ridgidNoiseSettings.persistence = Random.Range(-.2f, .7f);
                ridgidNoiseSettings.center = randomVect3();
                ridgidNoiseSettings.minValue = Random.Range(.2f, .9f);
                ridgidNoiseSettings.weightMultiplier = Random.Range(.2f, .9f);
                //Debug.Log("RandomRidgid");
            }
        }
        else if (type == "Earth")
        {

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
                simpleNoiseSettings.center = randomVect3();
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
                ridgidNoiseSettings.center = randomVect3();
                ridgidNoiseSettings.minValue = Random.Range(0, .5f);
                ridgidNoiseSettings.weightMultiplier = Random.Range(.5f, .8f);
                //Debug.Log("RandomRidgid");
            }
        }
        
    }
}
