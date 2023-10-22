using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private string quest; // Important WOrd just delete dis prolly
    private string answ; // Holder for Player's Answer
    List<char> lettersRand = new List<char>(); // For randomizing the word each letter
 
    // Add options  button for settings
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;

    //Text File Database
    [SerializeField] TextAsset dictionary;
    private System.Collections.Generic.HashSet<string> word;
    string alphabet = "ABCDEFGHIJKLMNOPRSTUVWYZAEIOUMNS";

    Characters characters;
    [SerializeField] GameObject top;

    bool dmgExtra=false;

    int x = 0;
    float pos; // position of buttons

    [SerializeField] TextMeshProUGUI getText;
    [SerializeField] TextMeshProUGUI setText;
    [SerializeField] TMP_InputField inputHere;


    private void Awake()
    {
        characters = top.GetComponent<Characters>();
    }

    void Start()
    {

        //inputHere.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
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

                if (word.Contains(answ.ToLower())&&answ.Length>=3) // Change this with Valid words // Thas goin to be Harddddddd // Use the 'valid' variable at top
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
                lettersButton[chosenLetters[x]].transform.position = Vector2.MoveTowards(lettersButton[chosenLetters[x]].transform.position, new Vector2(-pos + (x * 1.2f), 1), 12f * Time.deltaTime);
                Vector3 hold = new Vector2(-pos + (x * 1.2f), 1);
            if (lettersButton[chosenLetters[chosenLetters.Count-1]].transform.position == hold)
                {
                    Debug.Log("Stop");
                    click = false;
                }
            }
           

        }
    }
        public void hit()
    {
        characters.attk = true;
        
        if(dmgExtra) { characters.hpEnemy -= (characters.dmgPlayer * 2); dmgExtra = false; }
        else { characters.hpEnemy -= characters.dmgPlayer; }
        if(!(characters.hpEnemy<=0)) { characters.hpPlayer -= characters.dmgEnemy;  }

        
        attack.interactable = false;

        for (int i = 0; i < chosenLetters.Count; i++)
        {
            int num = random.Next(0, alphabet.Length - 1);
            lettersButton[chosenLetters[i]].transform.position = lettersPosition[chosenLetters[i]];
            letters[chosenLetters[i]].text = alphabet[num]+"";
        }

        answ = "";
        chosenLetters.Clear();
    }

    public void PotionHeal()
    {

        if (characters.hpPlayer >= 100)
        {

        }
        else
        {
            characters.hpPlayer += 20f;
            if (characters.hpPlayer > 100)
            {
                characters.hpPlayer = 100f;
            }
            //PlayerPrefs.SetFloat("playerHP", hpPlayer);
            characters.potion = true;
        }
    }
    public void PotionDamage()
    {
        dmgExtra = true;
    }

    public void Questions()
    {
        string sets = "";
        int i = 0;
        quest = ""; // Change this with the important word

        for (i = 0; i < alphabet.Length; i++) // Randomize other Letters
        {
            int num = random.Next(0, alphabet.Length - 1);
            if (i < 16 - quest.Length)
            {
                sets += alphabet[num];
            }     
        }
        
        lettersRand.AddRange(sets+quest);
        
        for(i =0;i<16;i++)
        {
            int num = random.Next(0, lettersRand.Count - 1);
            char temp = lettersRand[num];
            lettersRand.RemoveAt(num);
            letters[i].text = temp.ToString();
        }
    }

    public void Shuffle()
    {
        List<string> tempo = new List<string>();

        for (int i = 0; i < 16; i++)
        {
            tempo.Add(letters[i].text);
            int num = random.Next(0, tempo.Count - 1);
            string temp = tempo[num];
            tempo.RemoveAt(num);
            tempo.Add(temp);
        }
        for (int i = 0; i < 16; i++)
        {
            letters[i].text = tempo[i];
        }
        
       
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
    }
    public void WordFinder()
    {
        string[] splitWord = new string[] { "\r\n", "\r", "\n" };
        string[] words = dictionary.text.Split(splitWord, System.StringSplitOptions.RemoveEmptyEntries);
        word = new System.Collections.Generic.HashSet<string>(words);

    }

    public void zhit()
    {
        setText.text = getText.text;
    }
    /*
    void ValueChangeCheck()
    {
        Debug.Log("Value Changed");
        inputHere.text = inputHere.text[0].ToString();
        setText.text = inputHere.text;
    }*/
    void LockInput()
    {
        if (inputHere.text.Length > 1)
        {
            setText.text = "Too Long";
        }
        else if (inputHere.text.Length == 0)
        {
            setText.text = "Main Input Empty";
        }
        else
        {
           setText.text = inputHere.text;
        }
    }
}
