using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    private Planet planet;
    private UltimatePlayer player;
    public ExteriorSpawner exterior_spawner;
    //private InteriorSpawner interior_spawner;

    private PaletteManager PM;
    public Canvas[] UI_array;
    /*
    public Canvas UI_mainMenu;
    public Canvas UI_gameover;
    public Canvas UI_player;
    public Canvas UI_settings;*/
    string CurrentPlanetType = "Random";
    private SwitchCam camManager;
    private bool isColorChanged = false;
    //public GameObject playerHold;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if( Instance == this )
        {
            Instance = null;
        }
    }

    void Start()
    {
        //GameMode = 0;

        Initialize();
        LoadMainMenu();


    }

    public void LoadMainMenu()
    {
        GameMode(0);
    }

    public void OpenSettings()
    {
        GameMode(1);
    }

    public void NewGame()
    {
        GameMode(2);
        /*
        enabled = true;
        ClearObject();
        //planet.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        //player.setStartPos(planet.currentRadius);
        exterior_spawner.gameObject.SetActive(true);
        */

    }

    public void GameOver()
    {
        GameMode(3);
        /*
        //planet.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        exterior_spawner.gameObject.SetActive(false);
        enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //NewGame();*/
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameOver();
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    void Initialize()
    {
        //playerHold.SetActive(true);
        player = FindAnyObjectByType<UltimatePlayer>();
        exterior_spawner = FindAnyObjectByType<ExteriorSpawner>();
        planet = FindAnyObjectByType<Planet>();
        PM = GetComponent<PaletteManager>();
        camManager = GetComponent<SwitchCam>();

        exterior_spawner.gameObject.SetActive(false);
        PM.gameObject.SetActive(true);
        player.gameObject.SetActive(false);

        UI_Display(0);
        
    }

    void UI_Display(int k) 
    {
        // 0: Main Menu
        // 1: Settings
        // 2: In-game
        // 3: Game Over
        for (int i = 0; i < UI_array.Length; i++)
        {
            if(i == k)
            {
                UI_array[i].enabled = true;
            }
            else
            {
                UI_array[i].enabled = false;
            }
        }
    }

    public void setCurrentPlanetTypeToRandom()
    {
        CurrentPlanetType = "Random";
        planet.RandomGeneratePlanet(CurrentPlanetType);
        isColorChanged = true;
    }

    public void setCurrentPlanetTypeToPalette()
    {
        CurrentPlanetType = "Palette";
        planet.RandomGeneratePlanet(CurrentPlanetType);
        isColorChanged = true;
    }

    public void setCurrentPlanetTypeToEarth()
    {
        CurrentPlanetType = "Earth";
        planet.RandomGeneratePlanet(CurrentPlanetType);
        isColorChanged = true;
    }

    public string getCurrentPlanetType()
    {
        return CurrentPlanetType;
    }

    void ClearObject()
    {
        Crater[] obstacles = FindObjectsOfType<Crater>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        Meteor[] mtrs = FindObjectsOfType<Meteor>();
        foreach (var mtr in mtrs)
        {
            Destroy(mtr.gameObject);
        }
    }

    

    void GameMode(int mode)
    {
        switch (mode)
        {
            // 0: Main Menu
            case 0:
                UI_Display(0);
                if(!isColorChanged)
                {
                    planet.RandomGeneratePlanet(CurrentPlanetType);
                }
                
                camManager.switchCam("Menu");
                exterior_spawner.gameObject.SetActive(false);
                planet.GetComponent<SelfRotate>().enabled = true;
                ClearObject();
                break;
            // 1: Settings
            case 1:
                UI_Display(1);
                isColorChanged = true;
                break;
            // 2: In Game
            case 2:
                UI_Display(2);
                planet.GetComponent<SelfRotate>().enabled = false;
                UI_array[2].GetComponentInChildren<Timer>().startTimer();
                //planet.RandomGeneratePlanet();
                camManager.switchCam("Game");
                enabled = true;
                
                player.initialize();
                player.gameObject.SetActive(true);
                
                exterior_spawner.gameObject.SetActive(true);
                exterior_spawner.GetComponent<ExteriorSpawner>().Launch();
                break;
            // 3: GameOver Menu
            case 3:
                UI_Display(3);

                player.gameObject.SetActive(false);
                exterior_spawner.gameObject.SetActive(false);
                enabled = false;
                isColorChanged = false;

                break;

            default:
                Debug.Log("Wrong GameMode Input");
                break;
        }

        
    }
}
