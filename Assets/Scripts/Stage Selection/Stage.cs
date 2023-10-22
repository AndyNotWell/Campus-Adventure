using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Stage : MonoBehaviour
{
    [SerializeField] Button[] levelButton;
    [SerializeField] TextMeshProUGUI[] buildingName;
    [SerializeField] GameObject infoBuilding;
    [SerializeField] GameObject border;
    [SerializeField] Button back;

    [SerializeField] Button startLevel;
    [SerializeField] Button backLevel;

    int indexStage; // index per stage :<

    [SerializeField] Camera navCam;
    private Vector3 dragOrigin;

    [SerializeField] SpriteRenderer stageMap;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        mapMinX = stageMap.transform.position.x - stageMap.bounds.size.x / 2f;
        mapMaxX = stageMap.transform.position.x + stageMap.bounds.size.x / 2f;

        mapMinY = stageMap.transform.position.y - stageMap.bounds.size.y / 2f;
        mapMaxY = stageMap.transform.position.y + stageMap.bounds.size.y / 2f;
    }

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

        if (!infoBuilding.active)
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
        PlayerPrefs.SetString("building", buildingName[indexStage].text);
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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = navCam.ScreenToWorldPoint(touch.position);
            }

            if (Input.touchCount == 1)
            {
                Vector3 difference = dragOrigin - navCam.ScreenToWorldPoint(touch.position);

                navCam.transform.position = ClampCamera(navCam.transform.position + difference);
                border.transform.position = new Vector3(navCam.transform.position.x, navCam.transform.position.y, border.transform.position.z);

                // navCam.transform.position += difference;
            }
        }
    }
    private Vector3 ClampCamera (Vector3 targetPosition)
    {
        float camHeight = navCam.orthographicSize;
        float camWidth = navCam.orthographicSize * navCam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    } 
}
