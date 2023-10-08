using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOrientation : MonoBehaviour
{
    bool screenOrientation;

    [SerializeField] RectTransform backButton;
    [SerializeField] RectTransform infoBuilding;
    [SerializeField] RectTransform infoCover;
    [SerializeField] RectTransform[] borders;

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
        backButton.anchorMax = new Vector2(.25f, .65f);
        infoBuilding.anchorMin = new Vector2(.025f, .15f);
        infoBuilding.anchorMax = new Vector2(.975f, .85f);
        infoCover.anchorMax = new Vector2(1, .915f);
        borders[0].anchorMin = new Vector2(0, 0.915f);
    }
    void LandscapeMode()
    {
        backButton.anchorMax = new Vector2(.25f, .8f);
        infoBuilding.anchorMin = new Vector2(.3f, 0.025f);
        infoBuilding.anchorMax = new Vector2(.7f, 0.975f);
        infoCover.anchorMax = new Vector2(1, .88f);
        borders[0].anchorMin = new Vector2(0, 0.88f);
    }
}
