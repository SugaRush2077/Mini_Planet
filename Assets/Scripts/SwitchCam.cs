using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwitchCam : MonoBehaviour
{
    public Camera MenuCam;
    public Camera ColorCam;
    public Camera GameCam;
    public Camera TestCam;

    private Vector3 defaultColorPos = new Vector3(0, 0, -105f);
    private Vector3 defaultMenuPos = new Vector3(0, 25f, -80f);
    private Vector3 defaultTutorialPos = new Vector3(0, 20f, -80f);
    private Vector3 originPos;
    private Vector3 targetPos;
    private int defaultMenuSize = 20;
    private int defaultTutorialSize = 10;

    private bool isTransition = false;
    [SerializeField]
    public float CameraPanTime;
    private float startTime;

    public void switchCam(string s)
    {
        deactivateAll();
        if(s == "Menu")
        {
            MenuCam.enabled = true;
            setToMenu();
        }
        else if (s == "Color")
        {
            MenuCam.enabled = true;
            //ColorCam.enabled = true;
            setToColor();
        }
        else if (s == "Game")
        {
            GameCam.enabled = true;
        }
        else if (s == "Tutorial")
        {
            MenuCam.enabled = true;
            setToTutorial();
        }
        else
        {
            Debug.Log("Wrong Camera Index!");
        }
    }

    void startLerp()
    {
        startTime = Time.time;
        originPos = MenuCam.transform.position;
        isTransition = true;
    }

    void setToMenu()
    {
        MenuCam.orthographic = true;
        MenuCam.orthographicSize = defaultMenuSize;
        targetPos = defaultMenuPos;
        startLerp();
    }

    void setToColor()
    {
        MenuCam.orthographic = false;
        MenuCam.fieldOfView = 60;
        targetPos = defaultColorPos;
        startLerp();
    }

    void setToTutorial()
    {
        MenuCam.orthographic = true;
        MenuCam.orthographicSize = defaultTutorialSize;
        targetPos = defaultTutorialPos;
        
        startLerp();
    }

    void FixedUpdate()
    {
        if (isTransition)
        {
            float t = (Time.time - startTime) / CameraPanTime;
            MenuCam.transform.position = new Vector3(Mathf.SmoothStep(originPos.x, targetPos.x, t)
                , Mathf.SmoothStep(originPos.y, targetPos.y, t), Mathf.SmoothStep(originPos.z, targetPos.z, t));

            // Old Method
            //LerpTimer += Time.deltaTime;
            //LerpTimer = LerpTimer / CameraPanTime;
            //MenuCam.transform.position = Vector3.Lerp(originPos, targetPos, LerpTimer / CameraPanTime);


            // From Menu to Tutorial
            if (originPos == defaultMenuPos && targetPos == defaultTutorialPos)
            {
                MenuCam.orthographicSize = Mathf.SmoothStep(defaultMenuSize, defaultTutorialSize, t);
            }
            // From Tutorial to Menu
            else if (originPos == defaultTutorialPos && targetPos == defaultMenuPos)
            {
                MenuCam.orthographicSize = Mathf.SmoothStep(defaultTutorialSize, defaultMenuSize, t);
            }


            if (MenuCam.transform.position == targetPos)
            {
                isTransition = false;
            }
        }
    }

    public void deactivateAll()
    {
        MenuCam.enabled = false;
        ColorCam.enabled = false;
        GameCam.enabled = false;
        
    }

    public void transitionToColor()
    {
        TestCam.orthographic = false;
        TestCam.fieldOfView = 60;
        isTransition = true;
    }

    public void transitionToMenu()
    {
        TestCam.orthographic = true;
        TestCam.orthographicSize = 20;
    }
}
