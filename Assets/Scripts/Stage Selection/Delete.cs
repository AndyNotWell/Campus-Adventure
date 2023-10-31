using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
   public void Save() {

        PlayerPrefs.SetInt("level", 10);
        Application.LoadLevel(Application.loadedLevel);
    }
    public void DeleteSave()
    {

        PlayerPrefs.SetInt("level", 1);
        Application.LoadLevel(Application.loadedLevel);
    }
}
