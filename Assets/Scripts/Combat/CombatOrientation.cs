using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatOrientation : MonoBehaviour
{
    bool screenOrientation;
    [SerializeField] RectTransform top;
    [SerializeField] RectTransform ground;
    [SerializeField] RectTransform bottom;
    [SerializeField] RectTransform[] pillars;

    [SerializeField] RectTransform letterHolder;
    [SerializeField] RectTransform actionBox;
    [SerializeField] RectTransform potionBox;

    GameObject[] characters;
    GameObject[] letters;

    private void Awake()
    {
        screenOrientation = Screen.orientation == ScreenOrientation.Portrait;
        characters = GameObject.FindGameObjectsWithTag("Characters");
        letters = GameObject.FindGameObjectsWithTag("Letters");
        
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
        top.anchorMin = new Vector2(0, .6f);
        ground.anchorMax = new Vector2(1, .35f);
        bottom.anchorMax = new Vector2(1, .6f);
        pillars[0].anchorMin = new Vector2(0, .95f);
        pillars[1].anchorMax = new Vector2(1, .05f);
        pillars[2].anchorMin = new Vector2(0, .75f);    
        pillars[2].anchorMax = new Vector2(1, .775f);
        pillars[3].anchorMin = new Vector2(0, .25f);    
        pillars[3].anchorMax = new Vector2(1, .275f);

        letterHolder.anchorMin = new Vector2(.05f, .275f);
        letterHolder.anchorMax = new Vector2(.95f, .75f);
        potionBox.anchorMin = new Vector2(.05f, .775f);
        potionBox.anchorMax = new Vector2(.95f, .95f);
        actionBox.anchorMin = new Vector2(.05f, .05f);
        actionBox.anchorMax = new Vector2(.95f, .25f);

        foreach (GameObject character in characters)
        {
            character.transform.localScale = new Vector3(100, 100, 1);
        }
        foreach (GameObject letter in letters)
        {
            letter.transform.localScale = new Vector3(2.5f, 2.5f, 1);
        }
    }
    void LandscapeMode()
    {
        top.anchorMin= new Vector2(0, .45f);
        ground.anchorMax = new Vector2(1, .775f);
        bottom.anchorMax = new Vector2(1    , .45f);
        pillars[0].anchorMin = new Vector2(0, .925f); // For Top Pillar
        pillars[1].anchorMax = new Vector2(1, .05f);  // For Bottom Pillar
        pillars[2].anchorMin = new Vector2(.33f, 0);    // For 2nd Pillar
        pillars[2].anchorMax = new Vector2(.375f, 1);
        pillars[3].anchorMin = new Vector2(.625f, 0);    // For 3rd Pillar
        pillars[3].anchorMax = new Vector2(.67f, 1);

        letterHolder.anchorMin = new Vector2(.375f, .05f);
        letterHolder.anchorMax = new Vector2(.625f, .95f);
        potionBox.anchorMin = new Vector2(.05f, .6f);
        potionBox.anchorMax = new Vector2(.33f, .95f);
        actionBox.anchorMin = new Vector2(.05f, .05f);
        actionBox.anchorMax = new Vector2(.33f, .6f);

        foreach (GameObject character in characters)
        {
            character.transform.localScale = new Vector3(60, 60, 1);
        }
        foreach (GameObject letter in letters)
        {
            letter.transform.localScale = new Vector3(1, 1, 1);
        } 
    }
}
