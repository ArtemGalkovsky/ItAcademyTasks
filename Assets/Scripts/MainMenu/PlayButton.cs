using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    internal class PlayButton : MonoBehaviour
    {
        [SerializeField] private string _gameSceneName = "Game";

        public void StartGame()
        {
            SceneManager.LoadScene(_gameSceneName);
        }
    }
}

