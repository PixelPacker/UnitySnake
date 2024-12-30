using UnityEngine;

namespace Project.Scripts.Game
{
    public class AppleController : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.AppleEaten();
                Destroy(this.gameObject);
            }
        }
    }
}