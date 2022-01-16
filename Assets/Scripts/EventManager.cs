using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    
    public static event Action<GameManager.GameState> OnGameStateChange = state => {}; 
    public static event Action OnSnakeTriggerEnter;
    public static event Action OnSnakeEat;

    private void Awake()
    {
        Instance = this;
    }

    public void GameStateChange(GameManager.GameState state)
    {
        if (OnGameStateChange != null)
            OnGameStateChange(state);
    }

    public void SnakeTriggerEnter()
    {
        if (OnSnakeTriggerEnter != null)
        {
            OnSnakeTriggerEnter();
        }
            

    }
    
    public void SnakeEat()
    {
        if (OnSnakeEat != null)
        {
            OnSnakeEat();
        }
            

    }
    
}
