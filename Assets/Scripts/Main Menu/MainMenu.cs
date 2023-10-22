using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] RectTransform[] menuButtonsPos;
    [SerializeField] Button Play;
    [SerializeField] Button Options;
    [SerializeField] Button Exit;


    bool screenOrientation = true;
    private void Awake()
    {
        screenOrientation = Screen.orientation == ScreenOrientation.Portrait;
    }
    private void Update()
    {
        if (Screen.orientation == ScreenOrientation.Portrait && screenOrientation == true)
        {
            //From Landscape To Portrait
            screenOrientation = false;
            PortraitMode();
        }
        else if ((Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) && (screenOrientation == false))
        {
            //From Portrait To Landscape
            screenOrientation = true;
            LandscapeMode();
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void PortraitMode()
    {
        menuButtonsPos[0].anchorMin = new Vector2(.2f, .8f);
        menuButtonsPos[0].anchorMax = new Vector2(.8f, 1f);
        menuButtonsPos[1].anchorMin = new Vector2(.2f, .5f);
        menuButtonsPos[1].anchorMax = new Vector2(.8f, .7f);
        menuButtonsPos[2].anchorMin = new Vector2(.2f, .2f);
        menuButtonsPos[2].anchorMax = new Vector2(.8f, .4f);
    }
    void LandscapeMode()
    {
        menuButtonsPos[0].anchorMin = new Vector2(.15f, .25f);
        menuButtonsPos[0].anchorMax = new Vector2(.35f, .75f);
        menuButtonsPos[1].anchorMin = new Vector2(.40f, .25f);
        menuButtonsPos[1].anchorMax = new Vector2(.60f, .75f);
        menuButtonsPos[2].anchorMin = new Vector2(.65f, .25f);
        menuButtonsPos[2].anchorMax = new Vector2(.85f, .75f);
    }
}
