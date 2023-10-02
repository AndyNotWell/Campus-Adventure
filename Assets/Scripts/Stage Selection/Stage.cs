using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stage : MonoBehaviour
{
    [SerializeField] Button[] levelButton;
    [SerializeField] GameObject infoBuilding;
    [SerializeField] GameObject border;
    [SerializeField] Button back;

    [SerializeField] Button startLevel;
    [SerializeField] Button backLevel;

    int indexStage; // index per stage :<

    [SerializeField] Camera navCam;
    private Vector3 dragOrigin;

    private void Start()
    {
        int x = 0;
        int levelAt = PlayerPrefs.GetInt("level");

        for (x = 0; x < levelButton.Length; x++)
        {
            if(x+1>levelAt)
            {
                levelButton[x].interactable = false;
            }
        }

        for (x = 0; x < levelButton.Length; x++)
        {
            int index = x;
            levelButton[x].onClick.AddListener(() => {

                indexStage = index;
                BuildingMenu();
            });
        }   
    }
    private void Update()
    {
        
        if (!infoBuilding.active) //For Camera // Disable if InfoBuilding is active
        {
            navCamera();
        }
    }

    public void BuildingMenu()
    {
        infoBuilding.gameObject.SetActive(true); 
    }
    public void PlayLevel()
    {
        PlayerPrefs.SetInt("round", 0);
        PlayerPrefs.SetInt("stage",indexStage); // Get the index of stage Clicked
        SceneManager.LoadScene(2);
    }
    public void BacktoLevel()
    {
        infoBuilding.gameObject.SetActive(false);
    }
    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void DeleteSave()
    {
        PlayerPrefs.SetInt("level", 1);
        Application.LoadLevel(Application.loadedLevel);
    }
    public void navCamera() // Probably different with phone touches!!! // Check ASAP
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragOrigin = navCam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - navCam.ScreenToWorldPoint(Input.mousePosition);

            navCam.transform.position += difference;
            border.transform.position += difference;
        }
    }
  

}
