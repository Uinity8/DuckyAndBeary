using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public TMP_Text timerText;
    private float timeElapsed;
    private bool isGameOver;

    public const string SetGameOverKey = "SetGameOver";

    private void Start()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        isGameOver = false;
        SignalManager.Instance.ConnectSignal(SetGameOverKey, SetGameOver);

    }

    void Update()
    {
        if (isGameOver) return;
        
        timeElapsed += Time.deltaTime;
        timerText.text = FormatTime(timeElapsed);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TogglePause()
    {
        UIManager.Instance.PauseGame();
        bool isPaused = UIManager.Instance.IsPaused;
        pausePanel.SetActive(isPaused);
    }

    public void SetGameOver(object[] args)
    {
        gameOverPanel.SetActive(true);
        isGameOver = true;
        SignalManager.Instance.DisconnectSignal(SetGameOverKey, SetGameOver);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToStageSelect()
    {
        UIManager.Instance.LoadScene("StartScene");
    }
}