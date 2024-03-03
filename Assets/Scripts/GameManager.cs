using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    private Player player;
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
        player = FindAnyObjectByType<Player>();
        exterior_spawner = FindAnyObjectByType<ExteriorSpawner>();
        interior_spawner = FindAnyObjectByType<InteriorSpawner>();
    }

    public void NewGame()
    {
        enabled = true;
        ClearObject();
        player.gameObject.SetActive(true);
        exterior_spawner.gameObject.SetActive(true);
        interior_spawner.gameObject.SetActive(true);
    }

    public void GameOver()
    {
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
