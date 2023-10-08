using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Characters : MonoBehaviour
{
    //For Player and Enemy
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerpoint; // Standing Point for the Player
    [SerializeField] GameObject enemypoint; // Standing Point for the Enemy

    // Player and Enemy HP // Player and Enemy Damage
    public int hpPlayer = 100, hpEnemy = 100, dmgPlayer = 10, dmgEnemy = 10;

    // Enemies each stage // Add Enemy // Add Stage
    [SerializeField] GameObject[] EnemyS1;
    [SerializeField] GameObject[] EnemyS2;
    [SerializeField] GameObject[] EnemyS3;
    List<GameObject> enemy = new List<GameObject>(); // Add every enemies inside one list
    int enemyIndex = 0;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI roundText;

    int stageAt, round = 0; //current stage and round
    int[] sizeStage; // Make a list of size each stage, for finding index too

    bool entrance = true, win, over = false;
    public bool attk = false;
    int turn = 0;
    float speed = 16f;

    [SerializeField] Button next;
    [SerializeField] GameObject cover;
    [SerializeField] GameObject endMenu;

    private void Start()
    {
        // Add all enemies in one tray or list
        enemy.AddRange(EnemyS1);
        enemy.AddRange(EnemyS2);
        enemy.AddRange(EnemyS3);

        sizeStage = new int[] { EnemyS1.Length, EnemyS2.Length, EnemyS3.Length }; // list the sizes each stage // need to manually add other stages :<
       
        stageAt = PlayerPrefs.GetInt("stage"); // Get the current stage
        
        EnemyIndex();
    }
    private void Update()
    {
        if (entrance) // For ENtrance DUhhhh
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, playerpoint.transform.position, Time.deltaTime * speed);
            enemy[enemyIndex + round].transform.position = Vector3.MoveTowards(enemy[enemyIndex + round].transform.position, enemypoint.transform.position, Time.deltaTime * speed);
            if ((player.transform.position == playerpoint.transform.position) && (enemy[enemyIndex + round].transform.position == enemypoint.transform.position))
            {
                entrance = false;
            }
        }
        if (attk) // Player or Enemy Attack // Observe this more, may find efficiet wayssss // Add animations here !!
        {
            switch (turn)
            {
                case 0: // Hitting the Enemy
                    player.transform.position = Vector3.MoveTowards(player.transform.position, enemy[enemyIndex + round].transform.position, Time.deltaTime * speed);
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
                    enemy[enemyIndex + round].transform.position = Vector3.MoveTowards(enemy[enemyIndex + round].transform.position, playerpoint.transform.position, Time.deltaTime * speed);
                    if (enemy[enemyIndex + round].transform.position == playerpoint.transform.position)
                    {
                        turn++;
                        if (hpEnemy <= 0) // For animation of death right after hit :D
                        {
                            win = false;
                        }
                    }
                    break;
                case 3: // Enemy Original Positon
                    enemy[enemyIndex + round].transform.position = Vector3.MoveTowards(enemy[enemyIndex + round].transform.position, enemypoint.transform.position, Time.deltaTime * speed);
                    if (enemy[enemyIndex + round].transform.position == enemypoint.transform.position)
                    {
                        if ((!win) && (hpEnemy <= 0))
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
                        if (round == sizeStage[stageAt])
                        {
                            Debug.Log("Next Stage");
                            if (levelSize == stageAt + 1)
                            {
                                
                                Debug.Log("Create new Level");
                                PlayerPrefs.SetInt("level", stageAt + 2);
                               

                            }
                            else if (sizeStage.Length == stageAt + 1)
                            {
                                Debug.Log("Final Game");
                                
                            }
                            else if(levelSize>stageAt + 1)
                            {
                                Debug.Log("Play Done Level");
                            }
                            else
                            {
                                Debug.Log("Error");
                            }

                            endMenu.SetActive(true);
                        }
                        else
                        {
                            next.gameObject.SetActive(true);
                            over = true;
                            Debug.Log("Here");
                        }

                    }
                    else if (!win)
                    {
                        Debug.Log("Enemy Wins");
                    }
                    attk = false;
                    break;
                default:
                    break;
            }

            cover.SetActive(attk); // Hide the cover after the attack 
        }
        //End of this attack bullshit
    }
    public void EnemyIndex()
    {

        levelText.text = "Stage " + (stageAt + 1);
        roundText.text = "Round " + (round + 1);
        hpEnemy = 10; // Change this to 100 after
        turn = 0;
        enemyIndex = 0;

        for (int x=0;x<stageAt;x++)
        {
            enemyIndex += sizeStage[x];
        }
    }
    public void Continue()
    {
        Debug.Log(enemyIndex + " " + (round-1) );
        enemy[enemyIndex + round-1].SetActive(false);
        entrance = true;
        next.gameObject.SetActive(false); over = false;
        EnemyIndex();
    }
}
