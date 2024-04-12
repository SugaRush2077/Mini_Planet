using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Windows;

public class LoadPalette : MonoBehaviour
{
    public string[] lineArray;
    public List<Palette> paletteList = new List<Palette>();
    /*
    public struct DataGroup
    {
        public string Name;
        public List<double[]> Values;
    }*/
    

    void Start()
    {
        TextAsset txt = Resources.Load("test1") as TextAsset;
        //Debug.Log(txt);
        // Save each line in lineArray
        lineArray = txt.text.Split('\n');

        // All lines in each cell
        for (int i = 0; i < lineArray.Length; i++)
        {
            // In each cell (line)
            string[] substring = new string[12];
            // Get each section from a line
            foreach (string line in lineArray)
            {
                substring = line.Split(',');
            }
            // Palette format: Name, NumOfColor, Color1, Color2, ... Color 10
            int num = 0;
            Int32.TryParse(substring[1], out num);
            string NewName = substring[0]; 
            Palette newPalette = new Palette(NewName, num);
            newPalette.initialize();

            for(int k = 0; k < num; k++)
            {
                newPalette.colorArray[k] = transHexColor(substring[2 + k]);
                Debug.Log(newPalette.colorArray[k]);
            }
            paletteList.Add(newPalette);
            //Debug.Log("name: " + i + paletteArray[i].name);
        }

        


    }

    Color transHexColor(string str)
    {
        if (str.Length != 6)
        {
            Debug.LogError("Invalid hex color format. It should be RRGGBB.");
        }
        float r = (float)System.Convert.ToInt32(str.Substring(0, 2), 16) / 255f;
        float g = (float)System.Convert.ToInt32(str.Substring(2, 2), 16) / 255f;
        float b = (float)System.Convert.ToInt32(str.Substring(4, 2), 16) / 255f;
        return new Color(r, g, b);
    }

}
