using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private const string highScore = "High Score";

    void Awake()
    {
        SetUpSingleton();
        IsTheGameStartedForTheFirstTime();
        //PlayerPrefs.DeleteAll();

    }

    void SetUpSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void IsTheGameStartedForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsTheGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt(highScore, 0);
            PlayerPrefs.SetInt("IsTheGameStartedForTheFirstTime", 0);
        }
    }

    public void SetHighscore(int score)
    {
        PlayerPrefs.SetInt(highScore, score);
    }
	
    public int GetHighScore() { return PlayerPrefs.GetInt(highScore); }
}
