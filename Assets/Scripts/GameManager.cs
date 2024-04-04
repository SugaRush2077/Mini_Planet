using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    private Planet planet;
    private UltimatePlayer player;
    private ExteriorSpawner exterior_spawner;
    private InteriorSpawner interior_spawner;
    
    //private float survivor_time = 0f;

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
        player = FindAnyObjectByType<UltimatePlayer>();
        exterior_spawner = FindAnyObjectByType<ExteriorSpawner>();
        interior_spawner = FindAnyObjectByType<InteriorSpawner>();
        planet = FindAnyObjectByType<Planet>();

        //planet.gameObject.SetActive(false);
        //player.gameObject.SetActive(false);
        exterior_spawner.gameObject.SetActive(false);
        interior_spawner.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        enabled = true;
        ClearObject();
        //planet.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        //player.setStartPos(planet.currentRadius);
        exterior_spawner.gameObject.SetActive(true);
        interior_spawner.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        //planet.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        exterior_spawner.gameObject.SetActive(false);
        interior_spawner.gameObject.SetActive(false);
        enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        NewGame();
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
        
    }

    void ClearObject()
    {
        Crater[] obstacles = FindObjectsOfType<Crater>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }
    }
}
