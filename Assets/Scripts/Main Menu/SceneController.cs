
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transition;
    [SerializeField] GameObject block;

    // Settings
    [SerializeField] GameObject settings;


    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public void ToMenu()
    {
        block.SetActive(true);
        StartCoroutine(toMenu());
    }
    public void ToStage()
    {
        block.SetActive(true);
        StartCoroutine(toStage());
    }
    
    public void ToCombat()
    {
        block.SetActive(true);
        StartCoroutine(toCombat());
    }
    IEnumerator toMenu()
    {

        transition.SetTrigger("Out");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        transition.SetTrigger("In");
        block.SetActive(false);
    }
    IEnumerator toStage()
    {
        transition.SetTrigger("Out");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
        transition.SetTrigger("In");
        block.SetActive(false);
    }
    IEnumerator toCombat()
    {

        transition.SetTrigger("Out");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
        transition.SetTrigger("In");
        block.SetActive(false);
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
    }
    public void CloseSettings()
    {
        settings.SetActive(false);
    }
}
