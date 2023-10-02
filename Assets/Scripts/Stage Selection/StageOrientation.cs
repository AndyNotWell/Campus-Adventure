using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOrientation : MonoBehaviour
{
  
    [SerializeField] RectTransform backButton;
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
    void PortraitMode()
    {
        backButton.anchorMax = new Vector2(.25f, .8f);
    }
    void LandscapeMode()
    {
        backButton.anchorMin = new Vector2(.025f, .2f);
        backButton.anchorMax = new Vector2(.1f, .8f);
    }
}
