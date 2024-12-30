using UnityEngine;
using UnityEngine.InputSystem;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    private Camera _mainCamera;
    private Sprite _appleSprite;

    public void Awake()
    {
        _mainCamera = Camera.main;
        _appleSprite = applePrefab.GetComponent<SpriteRenderer>().sprite;
        GameManager.Instance.appleSpawner = this;
        
        if (GameManager.Instance.apple == null)
        {
            SpawnApple();
        }
    }
    public void SpawnApple()
    {
        float gridSize = _appleSprite.bounds.size.x;
        float cameraWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
        float cameraHeight = _mainCamera.orthographicSize;

        float gridWidth = cameraWidth * 2 / gridSize;
        float gridHeight = cameraHeight * 2 / gridSize;
        
        float randomX = Mathf.Floor(Random.Range(-gridWidth / 2, gridWidth / 2)) * gridSize;
        float randomY = Mathf.Floor(Random.Range(-gridHeight / 2, gridHeight / 2)) * gridSize;
        
        randomX = Mathf.Clamp(randomX, -cameraWidth + gridSize / 2, cameraWidth - gridSize / 2);
        randomY = Mathf.Clamp(randomY, -cameraHeight + gridSize / 2, cameraHeight - gridSize / 2);
        Vector2 applePosition = new Vector2(randomX, randomY);
        if(GameManager.Instance.isPlayerThere(applePosition))
        {
            SpawnApple();
            return;
        }
        
        GameObject apple = Instantiate(applePrefab, applePosition, Quaternion.identity);
        GameManager.Instance.apple = apple;
    }
    
    public void SpawnAppleDebug(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SpawnApple();
        }
    }
}
