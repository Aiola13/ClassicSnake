using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    public Transform segmentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        // EventManager.OnSnakeEat += Grow;
        // EventManager.OnGameStateChange += OnTriggerEnter2D;
        EventManager.OnGameStateChange += Grow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Z)))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        SnakeMovement();
    }

    private void SnakeMovement()
    {
        this.transform.position = new Vector2(
            (Mathf.Round(this.transform.position.x) + _direction.x),
            (Mathf.Round(this.transform.position.y) + _direction.y));
    }

    private void Grow(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Eat)
        {
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = _segments[_segments.Count - 1].position;
            _segments.Add(segment);
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            //EventManager.Instance.SnakeEat();
            EventManager.Instance.GameStateChange(GameManager.GameState.Eat);
        }
            
        
        if(other.CompareTag("Wall"))
        {
            // EventManager.Instance.SnakeTriggerEnter();
            EventManager.Instance.GameStateChange(GameManager.GameState.Collision);
        }
    }
    
}
