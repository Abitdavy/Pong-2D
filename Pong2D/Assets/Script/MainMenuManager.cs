using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [Header("Main Menu Panel List")]
    public GameObject MainPanel;
    public GameObject HTTPanel;
    public GameObject TimerPanel;

    [Header("Pick Ball List")]
    public GameObject PickBallPanel;

    public GameObject volley;
    public GameObject basket;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        MainPanel.SetActive(true);
        HTTPanel.SetActive(false);
        TimerPanel.SetActive(false);
        PickBallPanel.SetActive(false);
    }

    public void SinglePlayerButton()
    {
        GameData.instance.isSinglePlayer = true;
        TimerPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void MultiPlayerButton()
    {
        GameData.instance.isSinglePlayer = false;
        TimerPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void BackButton()
    {
        HTTPanel.SetActive(false);
        TimerPanel.SetActive(false);
        PickBallPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }


    public void pickBall(string name)
    {
        if (name == ("basket"))
        {
            GameData.instance.BallType = basket;
        }

        else if (name == ("volley"))
        {
            GameData.instance.BallType = volley;
        }
        else
            GameData.instance.BallType = ball;

        PickBallPanel.SetActive(false);
        HTTPanel.SetActive(true);
    }    
    public void SetTimerButton(float timer)
    {
        GameData.instance.gameTimer = timer;
        HTTPanel.SetActive(false);
        TimerPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();

        PickBallPanel.SetActive(true);
    }

    public void StarBtn()
    {
        SceneManager.LoadScene("Gameplay");
        SoundManager.instance.UIClickSfx();
    }

    public void ExitGame()
    {
        Application.Quit();
        SoundManager.instance.UIClickSfx();
    }

    public void RewardAd()
    {
        Debug.Log("Reward Ad...");
        GameData.instance.p1score = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
