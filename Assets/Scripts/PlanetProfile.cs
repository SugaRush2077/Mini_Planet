using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlanetProfile : MonoBehaviour
{
    public TMP_Text planetNameText;
    private string currentPlanetPalette;
    private int currentPlanetRadius;
    //private int currentPlanetRadius;
    private Planet planet;

    private void Start()
    {
        Planet.whenCompletedGeneratePlanet += OnCCompleted;
    }

    private void OnDestroy()
    {
        Planet.whenCompletedGeneratePlanet -= OnCCompleted;
    }
    
    private void OnCCompleted()
    {
        planet = FindAnyObjectByType<Planet>();
        generateName();
    }

    void generateName()
    {
        //timeGen();
        //Debug.Log(planet.currentPaletteName);
        //Debug.Log(planet.currentRadius.ToString());
        
        string s = planet.currentPaletteName;
        char ss = s[UnityEngine.Random.Range(0, s.Length)];
        planetNameText.text = s + planet.currentRadius.ToString() + "_" + timeGen().ToString() + ss;
    }

    int timeGen()
    {
        DateTime currentTime = DateTime.Now;
        int s = currentTime.Second;
        if (s == 0)
        {
            s = 1;
        }
        return (((currentTime.Year + currentTime.Month) - currentTime.Day) * (currentTime.Hour + currentTime.Minute)) / s;
    }

    
}
