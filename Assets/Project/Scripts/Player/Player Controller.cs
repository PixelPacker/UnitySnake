using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Renderer _renderer;
    
    public float moveSpeed = 0.5f;
    public Vector2 dir = new Vector2(0, 1);
    public float moveDelay = 0.25f;
    private float _lastMoved;
    

    public GameObject snakeBodyPrefab;
    public List<GameObject> snakeBody = new List<GameObject>();
    public Vector2 segmentSpawnLocation;
    private void Awake()
    {
        GameManager.Instance.player = this;
        moveSpeed = spriteRenderer.sprite.bounds.size.x;
        _lastMoved = Time.fixedTime + moveDelay;
        _renderer = GetComponent<Renderer>();
        snakeBody.Add(this.gameObject);
    }

    void Update()
    {
        if (Time.fixedTime >= _lastMoved + moveDelay)
        {
            segmentSpawnLocation = snakeBody[snakeBody.Count-1].transform.position;
            Vector2 playerPos = transform.position;
            transform.position = playerPos + (dir * moveSpeed);
            for (int i = 1; i < snakeBody.Count; i++)
            {
                Vector2 temp = snakeBody[i].transform.position;
                snakeBody[i].transform.position = playerPos;
                playerPos = temp;
            }
            GameManager.Instance.GameTick();
            _lastMoved = Time.fixedTime;
        }
    }

    public void playerMoved(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        if (input != Vector2.zero && input != -dir)
        {
            dir = input;
        }
    }

    public void GrowSnakeDebug(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GrowSnake();
        }
    }

    public void GrowSnake()
    {
        GameObject segment = Instantiate(snakeBodyPrefab);
        segment.transform.position = segmentSpawnLocation;
        snakeBody.Add(segment);
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("body"))
        {
            GameManager.Instance.EndGame();
        }
    }
}
