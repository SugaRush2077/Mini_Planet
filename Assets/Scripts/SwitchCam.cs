using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public Camera GameCam;
    public Camera MenuCam;
    
    public void switchCam(string s)
    {
        deactivateAll();
        if(s == "Menu")
        {
            MenuCam.enabled = true;
        }
        else if (s == "Game")
        {
            GameCam.enabled = true;
        }
        else
        {
            Debug.Log("Wrong Camera Index!");
        }
    }

    public void deactivateAll()
    {
        GameCam.enabled = false;
        MenuCam.enabled = false;
    }
}
