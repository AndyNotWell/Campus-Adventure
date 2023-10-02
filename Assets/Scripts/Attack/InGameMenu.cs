using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject[] backToMenu;
    [SerializeField] GameObject pauseMenu;

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
    }
    public void BackToStage()
    {
        SceneManager.LoadScene(0);
    }
}