using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class Attack : MonoBehaviour
{
    System.Random random = new System.Random(); //For Random DUhh

    //For Buttons and Text
    [SerializeField] Button attack;
    [SerializeField] Button shuffle;

    [SerializeField] Button[] lettersButton; //Buttons for the Letters
    List<Vector2> lettersPosition = new List<Vector2>();
    bool click = false;

    [SerializeField] TextMeshProUGUI[] letters;
    List<int> chosenLetters = new List<int>(); // List of chose Letters or Button
    private string answ; // Holder for Player's Answer

    // Add options  button for settings
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;

    string alphabet = "XSABJCDEGHIKLMNOPRSTUVAEYOZAEIWOMNFU";
    List<string> word = new List<string>();
    List<string> definition = new List<string>();
    int wordIndex;

    // For Dictionary
    [SerializeField] TextMeshProUGUI displayWord;
    [SerializeField] TextMeshProUGUI displayDefinition;
    [SerializeField] TextAsset dictionaryWords;
    [SerializeField] TextAsset dictionaryDefinitions;



    Characters characters;
    [SerializeField] GameObject top;

    bool dmgDouble=false;

    int x = 0;
    float pos; // position of buttons

    // FOR EXTRA LETTER POTION
    [SerializeField] Button specialLetterButton; 
    [SerializeField] TextMeshProUGUI specialLetter;
    [SerializeField] TMP_InputField inputHere;
    [SerializeField] GameObject potionLetter; //simple image of potion // not clickable

    int potionH, potionD, potionL;
    [SerializeField] TextMeshProUGUI[] potionCount;


    private void Awake()
    {
        characters = top.GetComponent<Characters>();
    }

    void Start()
    {
        potionH = PlayerPrefs.GetInt("potionHeal"); potionCount[0].text = potionH + "";
        potionD = PlayerPrefs.GetInt("potionDamage"); potionCount[1].text = potionD + "";
        potionL = PlayerPrefs.GetInt("potionLetter"); potionCount[2].text = potionL + "";
        if (potionH == 0) { }
        if (potionD == 0) { }
        if (potionL == 0) { inputHere.gameObject.SetActive(false); } else { inputHere.gameObject.SetActive(true); }

        inputHere.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputHere.onEndEdit.AddListener(delegate { LockInput(); });

        for (x = 0; x < lettersButton.Length; x++)
        {
            int index = x;
            lettersPosition.Add(lettersButton[x].transform.position); // Get the Buttons original Postion
            lettersButton[x].onClick.AddListener(() => { // use this for button actions
                
                if (!chosenLetters.Contains(index)) // Adding letters to the player's answer
                {
                    chosenLetters.Add(index);
                }
                else // remove the letter to your answer
                {
                    lettersButton[index].transform.position = lettersPosition[index];
                    chosenLetters.Remove(index);
                }

                answ = "";
                pos = (chosenLetters.Count - 1) * .6f; // Great fucking idea, position the button perfectly check your notes dumdum
                for (x = 0; x < chosenLetters.Count; x++)
                {
                    answ += letters[chosenLetters[x]].text;
                }
                click = true;

                if ((word.Contains(answ.ToLower()))&&(answ.Length>=3)) // Change this with Valid words // Thas goin to be Harddddddd // Use the 'valid' variable at top
                {
                    attack.interactable = true; // shows the attack button
                }
                else
                {
                    attack.interactable = false; // hide the attack button
                }
                
               
            });
        }
        
        WordFinder();
        Questions();

    }
    private void Update()
    {
        if (click)
        {
            for (x = 0; x < chosenLetters.Count; x++)
            {
                lettersButton[chosenLetters[x]].transform.position = Vector2.MoveTowards(lettersButton[chosenLetters[x]].transform.position, new Vector2(-pos + (x * 1.2f), 1), 15f * Time.deltaTime);
                Vector3 hold = new Vector2(-pos + (x * 1.2f), 1);
            if (lettersButton[chosenLetters[chosenLetters.Count-1]].transform.position == hold)
                {
                    click = false;
                }
            }
        }
       
        
    }
    public void hit()
    {
        float dmgExtra = (answ.Length - 3) * 10;// 5 added dmg each letter 
        string betterLetter = "ZXYKWQV";
        wordIndex = word.IndexOf(answ.ToLower());
        for(int i=0;i<betterLetter.Length;i++)
        {
            if (answ.Contains(betterLetter[i]))
            {
                dmgExtra += 5;
            }
        }

        characters.attk = true;
        
        if(dmgDouble) { characters.hpEnemy -= ((characters.dmgPlayer + dmgExtra) * 2); dmgDouble = false; } // For character's damage with double or not
        else { characters.hpEnemy -= (characters.dmgPlayer+dmgExtra); }
        
        if(characters.hpEnemy>0) 
        { 
            characters.hpPlayer -= characters.dmgEnemy;  
        } 
        else
        {
            Debug.Log("Enemy  Died");
            RandomPotion();
        }
        if(specialLetterButton.transform.position.y  > .9f) // If Special Letter is used
        {
            potionLetter.SetActive(true);
            inputHere.gameObject.SetActive(true);
            inputHere.text = "";
            specialLetterButton.gameObject.SetActive(false); // Disable the special letter
            
        }
       

        attack.interactable = false;

        for (int i = 0; i < chosenLetters.Count; i++)
        {
            int num = random.Next(0, alphabet.Length - 1);
            lettersButton[chosenLetters[i]].transform.position = lettersPosition[chosenLetters[i]];
            letters[chosenLetters[i]].text = alphabet[num]+"";
        }

        displayWord.text = answ;
        displayDefinition.text = definition[wordIndex];
        answ = "";
        chosenLetters.Clear();
    }

    

    public void Questions()
    {
      

        for (int i = 0; i < alphabet.Length; i++) // Randomize other Letters
        {
            int num = random.Next(0, alphabet.Length - 1);
            if (i < 16)
            {
                letters[i].text = alphabet[num].ToString();
            }     
        }
    }

    public void Shuffle()
    {
        List<string> tempo = new List<string>();

        for (int i = 0; i < 16; i++)
        {
            if (!(lettersButton[i].transform.position.y > .9f))
            {
                tempo.Add(letters[i].text);
                int num = random.Next(0, tempo.Count - 1);
                string temp = tempo[num];
                tempo.RemoveAt(num);
                tempo.Add(temp);
            }
            
        }
        Debug.Log(tempo);
        for (int j=0, i = 0; i < 16; i++)
        {
            if (!(lettersButton[i].transform.position.y > .9f))
            {
                letters[i].text = tempo[j];
                j++;
            }
        }
        
       
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
    }
    public void WordFinder()//Problem
    {
        string[] splitWord = new string[] { "\r\n", "\r", "\n" };
        string[] words = dictionaryWords.text.Split(splitWord, System.StringSplitOptions.RemoveEmptyEntries);
        string[] definitions = dictionaryDefinitions.text.Split(splitWord, System.StringSplitOptions.RemoveEmptyEntries);
        word = words.ToList(); definition = definitions.ToList(); 
    }
    public void PotionHeal()
    {
        if((potionH>0)&&(characters.hpPlayer < 100))
        {
            potionH -= 1;
            characters.hpPlayer += 20f;

            if (characters.hpPlayer > 100)
            {
                characters.hpPlayer = 100f;
            }
            //PlayerPrefs.SetFloat("playerHP", hpPlayer);
            characters.potion = true;
            PlayerPrefs.SetInt("potionHeal", potionH);
            potionCount[0].text = potionH.ToString();
            if (potionH == 0) { }
        }
        else
        {
            Debug.Log("Out of Heal Potion or Max HP");
        }
        }
    public void PotionDamage()
    {
        if ((!dmgDouble) && (potionD > 0))
        {
            potionD -= 1;
            dmgDouble = true;
            PlayerPrefs.SetInt("potionDamage", potionD);
            potionCount[1].text = potionD.ToString();
            if (potionD == 0) { }
        }
        else
        {
            Debug.Log("Double Damage is Active or Out of Damage Potion");
        }

    }
    void ValueChangeCheck() // For extra letter potion
    {
        inputHere.text = inputHere.text.ToString().ToUpper();
    }
    void LockInput()
    {
        if (inputHere.text.Length > 1) // if special letter is greater than 1
        {
            inputHere.text = "";
        }
        else if (inputHere.text.Length == 0) // if special letter is 0
        {
            inputHere.text = "";
        }
        else
        {
            potionL -= 1;
            specialLetter.text = inputHere.text;
            inputHere.gameObject.SetActive(false);
            potionLetter.SetActive(false);
            specialLetterButton.gameObject.SetActive(true);
            PlayerPrefs.SetInt("potionLetter", potionL);
            potionCount[2].text = potionL.ToString();
        }
    }
    public void RandomPotion()
    {

        int num = random.Next(1, 4);
        switch (num)
        {
            case 1:

                potionH += 1;
                PlayerPrefs.SetInt("potionHeal", potionH);
                potionCount[0].text = potionH + "";
                break;

            case 2:

                potionD += 1;
                PlayerPrefs.SetInt("potionDamage", potionD);
                potionCount[1].text = potionD + "";
                break;

            case 3:
                potionL += 1;
                PlayerPrefs.SetInt("potionLetter", potionL);
                potionCount[2].text = potionL + "";
                break;

            default:
                break;

        }
        Debug.Log(num);
    }
}
