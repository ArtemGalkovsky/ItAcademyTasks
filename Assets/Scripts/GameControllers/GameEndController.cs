using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameEndController : MonoBehaviour
    {
        [SerializeField] private string _mainMenuSceneName = "MainMenu";
        [SerializeField] private float _secondsToEndGame = 1.5f;
        
        public void EndGame()
        {
            Debug.Log("GAME OVER!");
            StartCoroutine(EndTheGame());
        }

        private IEnumerator EndTheGame()
        {
            yield return new WaitForSeconds(_secondsToEndGame);
            SceneManager.LoadScene(_mainMenuSceneName);
        }
    }
   
}
