using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 10f;
    public bool DefaultRandomize = true;
    public NoiseLayer[] noiseLayers;
    
    const int NumOfNoiseLayer = 3;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;
        public bool useFirstLayerAsMask;
        public NoiseSettings noiseSettings;
        public NoiseLayer() {
            noiseSettings = new NoiseSettings();
        }
    }

    public ShapeSettings()
    {
        
        noiseLayers = new NoiseLayer[NumOfNoiseLayer];
    }

    public void RandomGenerateNoiseLayer()
    {
        //noiseLayers = new NoiseLayer[3];
        for(int i = 0; i < noiseLayers.Length; i++)
        {
            //noiseLayers[i] = new NoiseLayer();
            //Debug.Log(noiseLayers[i]);
            noiseLayers[i].enabled = true;
            if (i != 0)
            {
                noiseLayers[i].useFirstLayerAsMask = true;
            }
            if (i != noiseLayers.Length - 1)
            {
                noiseLayers[i].noiseSettings.filterType = NoiseSettings.FilterType.Simple;
            }
            else
            {
                noiseLayers[i].noiseSettings.filterType = NoiseSettings.FilterType.Ridgid;
            }
                
            noiseLayers[i].noiseSettings.RandomizeSettings(DefaultRandomize);
        }
    }

}
