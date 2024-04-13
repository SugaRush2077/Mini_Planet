using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Windows;

public class PaletteManager : MonoBehaviour
{
    public static PaletteManager Instance { get; private set; }
    [HideInInspector]
    public string[] lineArray;
    [HideInInspector]
    //public List<Palette> paletteList = new List<Palette>();
    public Palette[] paletteArray;
    [HideInInspector]
    public string fileName = "PaletteDataset";
    [HideInInspector]
    public int num_of_palette;
    
    private void Awake()
    {
        LoadFile();
        AddPalette();
    }

    void LoadFile()
    {
        Debug.Log("Load palette from text...");
        TextAsset txt = Resources.Load(fileName) as TextAsset;
        //Debug.Log(txt);
        // Save each line in lineArray
        lineArray = txt.text.Split('\n');
        num_of_palette = lineArray.Length;
        paletteArray = new Palette[num_of_palette];
    }

    void AddPalette()
    {
        //Debug.Log(lineArray.Length);
        // All lines in each cell
        for (int i = 0; i < lineArray.Length; i++)
        {
            // Get each section from a line
            string[] substring = new string[12];
            substring = lineArray[i].Split(',');

            // Palette format: Name, NumOfColor, Color1, Color2, ... Color 10
            int num = 0;
            Int32.TryParse(substring[1], out num);
            string NewName = substring[0];

            Palette newPalette = ScriptableObject.CreateInstance<Palette>();
            newPalette.name = NewName;
            newPalette.colorAmount = num;
            //Debug.Log("New Palette will have " + newPalette.colorAmount + " colors");
            newPalette.initialize();

            for (int k = 0; k < num; k++)
            {
                newPalette.colorArray[k] = transHexColor(substring[2 + k]);
            }
            //paletteList.Add(newPalette);
            paletteArray[i] = newPalette;
            //Debug.Log("name: " + i + paletteArray[i].name);
        }
    }

    void printPaletteList()
    {
        /*
        Debug.Log("Print All Palettes in List");
        foreach(var p in paletteList)
        {
            Debug.Log("Palette Name: " + p.name);
            Debug.Log("Palette NumOfColor: " + p.colorAmount);
            foreach(var c in p.colorArray)
            {
                Debug.Log(c);
            }
        }*/
    }

    Color transHexColor(string str)
    {
        if (str.Length != 6)
        {
            Debug.LogError("Invalid hex color format. It should be RRGGBB.");
            Debug.LogError("Input String: " + str + " length: " + str.Length);
        }
        float r = (float)System.Convert.ToInt32(str.Substring(0, 2), 16) / 255f;
        float g = (float)System.Convert.ToInt32(str.Substring(2, 2), 16) / 255f;
        float b = (float)System.Convert.ToInt32(str.Substring(4, 2), 16) / 255f;
        return new Color(r, g, b);
    }

}
