using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    [Header("Game Settings")]
    [Tooltip("Speed value between 0.02 and 0.04.")]
    [Range(0.04f, 0.9f)]
    public float speed;
    
    //public delegate void OnGameStateChanged(GameManager.GameState state);
    //public static event OnGameStateChanged OnStateChange;

    // public static event Action<GameState> OnGameStateChanged = state => {}; 
    //public static event Action<Collider2D> OnTriggerEnter2DEvent = other => {};
    

    public enum GameState
    {
        Start, // Game start
        Pause, //Game in pause
        Victory, //Victory condition
        Lose, //Failure condition 
        InGame, //PLayer is playing
        Collision, //Snake strikes an obstacle
        Eat //Snake eats food
    }
    // make sure the constructor is private, so it can only be instantiated here
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.Eat:
                break;
            case GameState.Start:
                break;
            case GameState.InGame:
                Time.timeScale = speed;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;  
            case GameState.Collision:
                
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        EventManager.Instance.GameStateChange(newState);
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = Time.fixedDeltaTime = 0.9f;
        // EventManager.OnGameStateChange += UpdateGameState;
        // UpdateGameState(GameState.InGame);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        if(Time.fixedDeltaTime != speed)
            Time.fixedDeltaTime = speed;
        
        
        if (Input.GetKeyDown(KeyCode.Escape) && state != GameState.Pause)
            UpdateGameState(GameState.Pause);
        else if (Input.GetKeyDown(KeyCode.Escape) && state == GameState.Pause)
            UpdateGameState(GameState.InGame);
    }
    
    public void OnApplicationQuit(){
        GameManager.Instance = null;
    }
    
    void OnValidate()
    {
        Debug.Log(state);
        UpdateGameState(state);
    }
}
