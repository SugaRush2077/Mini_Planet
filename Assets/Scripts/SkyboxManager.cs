using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField]
    public Material[] skyMaterialArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void randomSkybox()
    {
        int r = Random.Range(0, skyMaterialArray.Length);
        RenderSettings.skybox = skyMaterialArray[r];
        

    }
    
}
