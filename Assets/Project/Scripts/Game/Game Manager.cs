using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Score = 0;

    public AppleSpawner appleSpawner;
    public GameObject apple;
    public PlayerController player;
    
    public void AppleEaten()
    {
        Score++;
        player.GrowSnake();
    }

    public static GameManager Instance;

    public void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void GameTick()
    {
        if (apple == null)
        {
            appleSpawner.SpawnApple();
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool isPlayerThere(Vector2 pos)
    {
        foreach (var segment in player.snakeBody)
        {
            if ((Vector2)segment.transform.position == pos)
            {
                return true;
            }
        }

        return false;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            EndGame();
        }
    }
}
