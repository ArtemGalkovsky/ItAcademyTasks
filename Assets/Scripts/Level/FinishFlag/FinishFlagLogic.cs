using Game;
using Player;
using UnityEngine;

namespace Level
{
    public class FinishFlagLogic : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerHealth>(out _))
            {
                var gameEndController = FindFirstObjectByType<GameEndController>();

                if (gameEndController != null)
                {
                    gameEndController.EndGame();
                    return;
                }
                
                Debug.LogError("Game end controller not found on scene!");
            }
        }
    }   
}

