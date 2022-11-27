using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Settings")]
    public int player1Score;
    public int player2Score;
    public float timer;
    public string pickBall;
    public bool isOver;
    public bool goldenGoal;
    public bool isSpawnPowerUp;
    public GameObject ballSpawned;

    [Header("Prefab")]
    public GameObject BallPrefab;
    public GameObject[] powerup;

    [Header("Panels")]
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    [Header("Ingame UI")]
    public TextMeshProUGUI timertxt;
    public TextMeshProUGUI p1scoretxt;
    public TextMeshProUGUI p2scoretxt;
    public GameObject goldenGoalUI;

    [Header("Game Over UI")]
    public GameObject p1WinUI;
    public GameObject p2WinUI;
    public GameObject youWin;
    public GameObject youLose;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }    
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        

        p1WinUI.SetActive(false);
        p2WinUI.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
        goldenGoalUI.SetActive(false);

        timer = GameData.instance.gameTimer;
        isOver = false;
        goldenGoal = false;

        BallPrefab = GameData.instance.BallType;
        player1Score = GameData.instance.p1score;

        SpawnBall();
    }

    private void Update()
    {
        p1scoretxt.text = player1Score.ToString();
        p2scoretxt.text = player2Score.ToString();
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            timertxt.text = string.Format("{0:00}:{01:00}", minutes, seconds);

            if(seconds % 20 == 0 && !isSpawnPowerUp)
            {
                StartCoroutine("SpawnPowerUp");
            }
        }
        if(timer <=0f && !isOver)
        {
            timertxt.text = "00:00";
            if (player1Score == player2Score)
            {
                if (!goldenGoal)
                {
                    goldenGoal = true;

                    Ball[] ball = FindObjectsOfType<Ball>();
                    for (int i = 0; i < ball.Length; i++)
                    {
                        Destroy(ball[i].gameObject);
                    }

                    goldenGoalUI.SetActive(true);

                    SpawnBall();
                }
            }
            else
            {
                GameOver();
            }
        }
    }

    public IEnumerator SpawnPowerUp()
    {
        isSpawnPowerUp = true;
        Debug.Log("Power Up");
        int rand = Random.Range(0, powerup.Length);
        Instantiate(powerup[rand], new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-2.35f, 2.25f), 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        isSpawnPowerUp = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.UIClickSfx();
    }

    public void BacktoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
        SoundManager.instance.UIClickSfx();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
        SoundManager.instance.UIClickSfx();
    }

    public void SpawnBall()
    {
        Debug.Log("Muncul Bola");
        StartCoroutine("DelaySpawn");
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
        if(ballSpawned == null)
        {
            ballSpawned = Instantiate(BallPrefab, Vector3.zero, Quaternion.identity);
        }
    }

   
    public void GameOver()
    {
        isOver = true;
        Debug.Log("Game Over");
        Time.timeScale = 0;
        SoundManager.instance.GameOverSfx();

        GameOverPanel.SetActive(true);

        if(!GameData.instance.isSinglePlayer)
        {
            if(player1Score > player2Score)
            {
                p1WinUI.SetActive(true);
            }
            if(player2Score > player1Score)
            {
                p2WinUI.SetActive(true);
            }
        }
        else
        {
            if(player1Score > player2Score)
            {
                youWin.SetActive(true);
            }    
            if(player1Score < player2Score)
            {
                youLose.SetActive(true);
            }
        }
    }
}
