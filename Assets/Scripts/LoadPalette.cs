using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Windows;

public class LoadPalette : MonoBehaviour
{
    [HideInInspector]
    public string[] lineArray;
    [HideInInspector]
    //public List<Palette> paletteList = new List<Palette>();
    public Palette[] paletteArray;
    public string fileName = "test1";
    /*
    public struct DataGroup
    {
        public string Name;
        public List<double[]> Values;
    }*/
    

    void Start()
    {
        paletteArray = new Palette[10];
        LoadPaletteFromTxt();
        //printPaletteList();
    }
    void LoadPaletteFromTxt()
    {
        TextAsset txt = Resources.Load(fileName) as TextAsset;
        //Debug.Log(txt);
        // Save each line in lineArray
        lineArray = txt.text.Split('\n');
        Debug.Log(lineArray.Length);
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
