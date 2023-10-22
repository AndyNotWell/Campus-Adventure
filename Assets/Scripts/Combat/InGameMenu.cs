using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
        
        [SerializeField] GameObject pauseMenu;

        public void Resume()
        {
            pauseMenu.gameObject.SetActive(false);
        }
        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void BackToStage()
        {
            SceneManager.LoadScene(1);
        }
        public void Retry()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
}