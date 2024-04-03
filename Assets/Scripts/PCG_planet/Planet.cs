using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public bool usePCG = false;
    public bool GenerateOnce = false;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    [HideInInspector]
    public float currentRadius;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;
    [HideInInspector]
    public bool shapeSettingFoldout;
    [HideInInspector]
    public bool colorSettingFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;


    ShapeSettings defaultShapeSettings;
    ColorSettings defaultColorSettings;


    private void Start()
    {
        if(usePCG)
        {
            InvokeRepeating(nameof(RandomGeneratePlanet), .5f, 2);
        }
        else if(GenerateOnce)
        {
            GeneratePlanet();
        }
        
        
    }

    // Procedural Generate Planet
    public void RandomGeneratePlanet()
    {
        shapeSettings.planetRadius = 40f;
        currentRadius = shapeSettings.planetRadius;
        Debug.Log(currentRadius);
        shapeSettings.RandomGenerateNoiseLayer();
        
        //colorSettings.GenerateColorSetting();

        Initialize();
        GenerateMesh();
        GenerateColor();
        Debug.Log("Generate New Planet!");
    }



    private void Initialize()
    {
        gameObject.tag = "PCG_Planet";
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);
        
        if (meshFilters == null || meshFilters.Length == 0)
        { 
            meshFilters = new MeshFilter[6]; 
        }
        
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.tag = "PlanetSurface";

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
                meshFilters[i].AddComponent<MeshCollider>();
                //meshFilters[i].AddComponent<MeshCollider>();
            }
            meshFilters[i].GetComponent<MeshCollider>().sharedMesh = meshFilters[i].sharedMesh;
            //meshFilters[i].GetComponent<MeshCollider>().convex = true;
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            
            // Choose which face rendering
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
        
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColor();
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColorSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColor();
        }
    }

    void GenerateMesh()
    {
        /*
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
        */
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColor()
    {
        /*
        foreach (MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.planetColor;
        }
        */
        colorGenerator.UpdateColors();
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].UpdateUVs(colorGenerator);
            }
        }
    }
}
