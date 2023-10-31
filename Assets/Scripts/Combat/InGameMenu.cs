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
        SceneController.instance.ToMenu();
        }
        public void BackToStage()
        {
        SceneController.instance.ToStage();
        }
        public void Retry()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        public void Settings()
        {
        SceneController.instance.OpenSettings();
     }   

}