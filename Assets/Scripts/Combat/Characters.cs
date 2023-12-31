using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Characters : MonoBehaviour
{
    // FOR PLAYER AND ENEMY
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerpoint; // Standing Point for the Player
    [SerializeField] GameObject enemypoint; // Standing Point for the Enemy

    [SerializeField] Image healthBarPlayer,healthBarEnemy;
    public float hpPlayer, hpEnemy;
    float hpPlayerMax = 100, hpEnemyMax = 100; // Player and Enemy HP 
    public float dmgPlayer = 30, dmgEnemy = 10; // Player and Enemy Damage
    float lerpSpeed = 20; // for smooth deduction of health

    // FOR ENEMIES EACH STAGE
    [SerializeField] GameObject[] enemyStage;
    [SerializeField] TextMeshProUGUI enemyNameText;
    GameObject enemy;

    // STAGE AND ROUND
    int stageAt, round = 0; // Current stage and round

    [SerializeField] TextMeshProUGUI buildingNameText;
    [SerializeField] GameObject[] buildingImage;

    bool entrance = true, win, gameOver = false;
    public bool attk = false, potion = false;
    int turn = 0;
    float speed = 16f;

    // Gameobject of Menus and Continues
    [SerializeField] Button next;
    [SerializeField] GameObject cover;
    [SerializeField] GameObject menuDone, menuStageClear, menuPlayerLose, menuFinalStage;

    private void Start()
    {
        
        stageAt = PlayerPrefs.GetInt("stage"); // Get the current stag
        enemy = enemyStage[stageAt].transform.GetChild(round).gameObject;
        for (int x = 0; x < buildingImage.Length; x++) { buildingImage[x].SetActive(false); }
        EnemyIndex();

        hpPlayer = PlayerPrefs.GetFloat("playerHP");
        hpEnemyMax += (20 * stageAt);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    private void Update()
    {

        if (entrance) // For ENtrance DUhhhh
        {
            hpEnemy = hpEnemyMax;
            HealthBarFiller();

            player.transform.position = Vector3.MoveTowards(player.transform.position, playerpoint.transform.position, Time.deltaTime * speed);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemypoint.transform.position, Time.deltaTime * speed);
            if ((player.transform.position == playerpoint.transform.position) && (enemy.transform.position == enemypoint.transform.position))
            {
                entrance = false;
            }
        }
        if (attk) // Player or Enemy Attack // Observe this more, may find efficiet wayssss // Add animations here !!
        {
            int stageRound = enemyStage[stageAt].transform.childCount;

            switch (turn)
            {
                case 0: // Hitting the Enemy
                    player.transform.position = Vector3.MoveTowards(player.transform.position, enemy.transform.position, Time.deltaTime * speed);
                    if (player.transform.position == enemypoint.transform.position)
                    {
                        turn++;
                        if (hpEnemy <= 0) // For animation of death right after hit :D
                        {
                            win = true;
                        }
                    }
                    break;
                case 1: // Back to Original Position

                    HealthBarFiller();
                    player.transform.position = Vector3.MoveTowards(player.transform.position, playerpoint.transform.position, Time.deltaTime * speed);

                   
                    if (player.transform.position == playerpoint.transform.position)
                    {
                        if (hpEnemy <= 0) // test if gameover or continue...
                        {
                            turn = 4;
                        }
                        else
                        {
                            turn++;
                        }
                    }
                    break;
                case 2: // Enemy hitting Player

                    
                    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, playerpoint.transform.position, Time.deltaTime * speed);
                    if (enemy.transform.position == playerpoint.transform.position)
                    {
                        
                        turn++;
                        if (hpPlayer <= 0) // For animation of death right after hit :D
                        {
                            win = false;
                        }
                        
                    }
                    break;
                case 3: // Enemy Original Positon

                    HealthBarFiller();
                    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemypoint.transform.position, Time.deltaTime * speed);
                    if (enemy.transform.position == enemypoint.transform.position)
                    {
                        if ((!win) && (hpPlayer <= 0))
                        {
                            turn = 4;
                            
                        }
                        else
                        {
                            attk = false;
                            turn = 0;
                        }
                    }
                    break;
                case 4: // When battle is over
                    if (win)
                    { 
                        round++;
                        int levelSize = PlayerPrefs.GetInt("level");
                        if (round == stageRound) // If entire stage is done // Else next round
                        {

                            PlayerPrefs.SetFloat("playerHP", hpPlayer);
                            Debug.Log("Next Stage");
                            if (levelSize == stageAt + 1)
                            {
                                Debug.Log("Create new Level");
                                PlayerPrefs.SetInt("level", stageAt + 2);
                            }
                            else if (enemyStage.Length == stageAt + 1)
                            {
                                Debug.Log("Final Game");
                                gameOver = true;
                                
                            }
                            else if(levelSize>stageAt + 1)
                            {
                                Debug.Log("Play Done Level");
                            }
                            else
                            {
                                Debug.Log("Error");
                            }

                            menuDone.SetActive(true);
                            if (!gameOver)
                            {
                                menuStageClear.SetActive(true);
                            }
                            
                            else if(gameOver)
                            {
                                menuFinalStage.SetActive(true);
                            }
                            else
                            {
                                Debug.Log("Error in MENUS");
                            }
                        }
                        else
                        {
                            next.gameObject.SetActive(true);         
                            Debug.Log("Here");
                        }

                    }
                    else if (!win)
                    {
                        Debug.Log("Enemy Wins");
                        menuDone.SetActive(true);
;                        menuPlayerLose.SetActive(true);
                    }
                    attk = false;
                    break;
                default:
                    break;
            }

            cover.SetActive(attk); // Hide the cover after the attack 
        }
        
        //End of this attack bullshit
        if(potion)
        {
            HealthBarFiller();
            potion = false;
        }
    }
    public void EnemyIndex()
    {
        //hpEnemy = 10; // Change this to 100 after
        turn = 0;

        buildingNameText.text = PlayerPrefs.GetString("building");
        enemyNameText.text = enemy.name;
        buildingImage[stageAt].SetActive(true);

    }
    public void Continue()
    {
        enemy.SetActive(false);
        enemy = enemyStage[stageAt].transform.GetChild(round).gameObject;
        entrance = true;
        next.gameObject.SetActive(false);
        EnemyIndex();
    }
    void HealthBarFiller() // For Quick Animation of Health and Colors
    {
        if(turn == 1)
        {
            healthBarEnemy.fillAmount = Mathf.Lerp(healthBarEnemy.fillAmount, hpEnemy / hpEnemyMax, (lerpSpeed * Time.deltaTime));
            Color healthColorEnemy = Color.Lerp(Color.red, Color.green, (hpEnemy / hpEnemyMax));
            healthBarEnemy.color = healthColorEnemy;
        }
        else if(turn == 3)
        {
            healthBarPlayer.fillAmount = Mathf.Lerp(healthBarPlayer.fillAmount, hpPlayer / hpPlayerMax, (lerpSpeed * Time.deltaTime));
            Color healthColorPlayer = Color.Lerp(Color.red, Color.green, (hpPlayer / hpPlayerMax));
            healthBarPlayer.color = healthColorPlayer;
        }
        else
        {
            healthBarEnemy.fillAmount = Mathf.Lerp(healthBarEnemy.fillAmount, hpEnemy / hpEnemyMax, (1f));
            Color healthColorEnemy = Color.Lerp(Color.red, Color.green, (hpEnemy / hpEnemyMax));
            healthBarEnemy.color = healthColorEnemy;
            healthBarPlayer.fillAmount = Mathf.Lerp(healthBarPlayer.fillAmount, hpPlayer / hpPlayerMax, (1f));
            Color healthColorPlayer = Color.Lerp(Color.red, Color.green, (hpPlayer / hpPlayerMax));
            healthBarPlayer.color = healthColorPlayer;
        }
    }

    
   
}
