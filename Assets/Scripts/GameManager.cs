using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




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
    private bool keepCurrentPlanet = false;
    SkyboxManager skybox;
    MusicSelector musicSelector;
    //public GameObject playerHold;
    public CameraShake cameraShake;

    public AudioClip[] audioClips;
    public AudioClip landing_audio;

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
        musicSelector = FindAnyObjectByType<MusicSelector>();
        skybox = GetComponent<SkyboxManager>();
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
        musicSelector.switchToGameMusic();
        SoundFXManager.instance.PlaySoundFXClip(landing_audio, transform, 1f);
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
    public void LoadTutorialMenu()
    {

        GameMode(4);
    }
    public void LoadSoundSettings()
    {
        GameMode(5);
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
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }
    void Initialize()
    {
        skybox.randomSkybox();
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

    void UI_Display(int mode) 
    {
        // 0: Main Menu
        // 1: Color
        // 2: In-game
        // 3: Game Over
        // 4: Tutorial
        // 5: Sound
        for (int i = 0; i < UI_array.Length; i++)
        {
            if(i == mode)
            {
                UI_array[i].enabled = true;
            }
            else
            {
                UI_array[i].enabled = false;
            }

            if(mode == 0 || mode == 1 || mode == 4 || mode == 5)
            {
                //SoundFXManager.instance.PlayRandomSoundFXClip(audioClips, transform, .2f);
            }
        }
    }

    public void setCurrentPlanetTypeToRandom()
    {
        CurrentPlanetType = "Random";
        planet.RandomGeneratePlanet(CurrentPlanetType);
        keepCurrentPlanet = true;
    }

    public void setCurrentPlanetTypeToPalette()
    {
        CurrentPlanetType = "Palette";
        planet.RandomGeneratePlanet(CurrentPlanetType);
        keepCurrentPlanet = true;
    }

    public void setCurrentPlanetTypeToEarth()
    {
        CurrentPlanetType = "Earth";
        planet.RandomGeneratePlanet(CurrentPlanetType);
        keepCurrentPlanet = true;
    }

    public string getCurrentPlanetType()
    {
        return CurrentPlanetType;
    }

    void ClearObstacle()
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
                SoundFXManager.instance.PlayRandomSoundFXClip(audioClips, transform, .25f);
                UI_Display(0);
                if(!keepCurrentPlanet)
                {
                    planet.RandomGeneratePlanet(CurrentPlanetType);
                }
                
                camManager.switchCam("Menu");
                exterior_spawner.gameObject.SetActive(false);
                planet.GetComponent<SelfRotate>().enabled = true;
                ClearObstacle();
                //cameraShake.ShakeCam();
                break;
            // 1: Colors
            case 1:
                SoundFXManager.instance.PlayRandomSoundFXClip(audioClips, transform, .25f);
                UI_Display(1);
                keepCurrentPlanet = true;
                camManager.switchCam("Color");
                break;
            // 2: In Game
            case 2:
                UI_Display(2);
                ClearObstacle();
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
                UI_array[2].GetComponentInChildren<Timer>().stopTimer();
                player.gameObject.SetActive(false);
                exterior_spawner.gameObject.SetActive(false);
                enabled = false;
                keepCurrentPlanet = false;

                break;
            // 4: Tutorial Menu
            case 4:
                SoundFXManager.instance.PlayRandomSoundFXClip(audioClips, transform, .25f);
                UI_Display(4);
                keepCurrentPlanet = true;
                camManager.switchCam("Tutorial");
                break;

            // 5: Sound Settings Menu
            case 5:
                SoundFXManager.instance.PlayRandomSoundFXClip(audioClips, transform, .25f);
                UI_Display(5);
                keepCurrentPlanet = true;
                camManager.switchCam("Sound");
                break;

            default:
                Debug.Log("Wrong GameMode Input");
                break;
        }

        
    }
}
