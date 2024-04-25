using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorSettings;

public class Palette
{
    // name, # color, color1, color2, ...., color10
    public string paletteName;
    public int colorAmount;
    public Color[] colorArray;

    //public Palette() {}
    
    public Palette(string str, int num)
    {
        this.paletteName = str;
        this.colorAmount = num;
        initialize();
    }

    public void initialize()
    {
        this.colorArray = new Color[this.colorAmount];
        for (int i = 0; i < this.colorAmount; i++)
        {
            this.colorArray[i] = Color.white;
            //this.colorArray[i].
        }
    }
    
    public void addColorInToPalette(Color newColor)
    {
        for (int i = 0; i < colorArray.Length; i++)
        {
            if (colorArray[i] == Color.white)
            {
                colorArray[i] = newColor;
                //Debug.Log(newColor);
                break;
            }
            else
            {
                continue;
            }
        }
    }
}
