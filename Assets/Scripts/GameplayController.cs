using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField] private Text scoreText, endScore, bestScore, gameOverText;
    [SerializeField] private Button resumeGameButton, instuctionButton;
    [SerializeField] private GameObject pausePanel;

    void Awake()
    {
        MakeInstance();
        Time.timeScale = 0f;
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        if (Player.instance != null)
        {
            if (Player.instance.isAlive)
            {
                pausePanel.SetActive(true);
                gameOverText.gameObject.SetActive(false);
                endScore.text = "" + Player.instance.score; //The same as .ToString()
                bestScore.text = "" + GameController.instance.GetHighScore();
                Time.timeScale = 0f;
                resumeGameButton.onClick.RemoveAllListeners();
                resumeGameButton.onClick.AddListener( (() => ResumeGame()));
            }
        }
    }

    public void HomeButton()
    {
        FadePanel.instance.FadeIn(0);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        FadePanel.instance.FadeIn(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayGame()
    {
        scoreText.gameObject.SetActive(true);
        instuctionButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

        endScore.text = "" + score;

        if (score > GameController.instance.GetHighScore())
        {
            GameController.instance.SetHighscore(score);
        }

        bestScore.text = "" + GameController.instance.GetHighScore();

        resumeGameButton.onClick.RemoveAllListeners();
        resumeGameButton.onClick.AddListener((() => RestartGame()));
    }
}
