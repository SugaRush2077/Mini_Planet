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
    private ExteriorSpawner exterior_spawner;
    //private InteriorSpawner interior_spawner;

    private PaletteManager PM;
    


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
        enabled = true;
        ClearObject();
        //planet.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        //player.setStartPos(planet.currentRadius);
        exterior_spawner.gameObject.SetActive(true);
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
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameOver();
        }
    }
    void Initialize()
    {
        player = FindAnyObjectByType<UltimatePlayer>();
        exterior_spawner = FindAnyObjectByType<ExteriorSpawner>();
        planet = FindAnyObjectByType<Planet>();
        PM = FindAnyObjectByType<PaletteManager>();
        PM.gameObject.SetActive(true);
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
                exterior_spawner.gameObject.SetActive(false);
                break;
            // 1: Selection
            case 1:

                break;
            // 2: In Game
            case 2:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                enabled = true;
                ClearObject();
                player.gameObject.SetActive(true);
                exterior_spawner.gameObject.SetActive(true);
                break;
            // 3: GameOver Menu
            case 3:
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
