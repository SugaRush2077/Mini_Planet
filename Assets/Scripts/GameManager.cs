using System.Collections;
using System.Collections.Generic;
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
    public Canvas UI_mainMenu;
    public Canvas UI_gameover;
    public Canvas UI_player;

    private SwitchCam camManager;
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
        GameMode(0);
        
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
        //planet.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        exterior_spawner.gameObject.SetActive(false);
        enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //NewGame();
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


        UI_mainMenu.enabled = true;
        UI_gameover.enabled = false;
        UI_player.enabled = false;
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
                camManager.switchCam("Menu");
                UI_mainMenu.enabled = true;
                UI_gameover.enabled = false;
                UI_player.enabled = false;
                exterior_spawner.gameObject.SetActive(false);
                planet.GetComponent<SelfRotate>().enabled = true;
                break;
            // 1: Selection
            case 1:
                UI_mainMenu.enabled = true;
                UI_gameover.enabled = false;
                UI_player.enabled = false;
                break;
            // 2: In Game
            case 2:
                //playerHold.SetActive(false);
                UI_mainMenu.enabled = false;
                UI_gameover.enabled = false;
                UI_player.enabled = true;
                //UI_player.GetComponentInChildren
                planet.GetComponent<SelfRotate>().enabled = false;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                camManager.switchCam("Game");
                enabled = true;
                ClearObject();
                player.gameObject.SetActive(true);

                exterior_spawner.gameObject.SetActive(true);
                exterior_spawner.GetComponent<ExteriorSpawner>().Launch();
                break;
            // 3: GameOver Menu
            case 3:
                UI_mainMenu.enabled = false;
                UI_gameover.enabled = true;
                UI_player.enabled = true;
                player.gameObject.SetActive(false);
                exterior_spawner.gameObject.SetActive(false);
                enabled = false;
                break;

            default:
                Debug.Log("Wrong GameMode Input");
                break;
        }

        
    }
}
