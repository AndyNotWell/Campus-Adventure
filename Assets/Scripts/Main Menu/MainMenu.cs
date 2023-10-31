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
    private void Start()
    {
        if(PlayerPrefs.GetInt("newGame")==0)
        {
            PlayerPrefs.SetInt("potionHeal", 2);
            PlayerPrefs.SetInt("potionDamage", 2);
            PlayerPrefs.SetInt("potionLetter", 2);
            PlayerPrefs.SetFloat("playerHP", 100);
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("newGame", 1);
        }
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
        SceneController.instance.ToStage();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Settings()
    {
        SceneController.instance.OpenSettings();
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
        menuButtonsPos[0].anchorMin = new Vector2(.4f, .55f);
        menuButtonsPos[0].anchorMax = new Vector2(.6f, .7f);
        menuButtonsPos[1].anchorMin = new Vector2(.4f, .35f);
        menuButtonsPos[1].anchorMax = new Vector2(.6f, .5f);
        menuButtonsPos[2].anchorMin = new Vector2(.4f, .15f);
        menuButtonsPos[2].anchorMax = new Vector2(.6f, .3f);
    }
}
