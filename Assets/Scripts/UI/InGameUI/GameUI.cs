using System;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public GameObject gameClearPanel;
    public TMP_Text timerText;
    private float timeElapsed;
    private bool isGameOver;
    private int totalGem;
    ItemController[] Gems;

    [SerializeField] private float missionTime;
    [SerializeField] private TextMeshProUGUI clearNum;
    [SerializeField] private TextMeshProUGUI gemNum;
    [SerializeField] private TextMeshProUGUI timeNum;
    [SerializeField] private TextMeshProUGUI clearCheck;
    [SerializeField] private TextMeshProUGUI gemCheck;
    [SerializeField] private TextMeshProUGUI timeCheck;


    public const string SetGameOverKey = "SetGameOver";
    public const string SetGameClearKey = "SetGameClear";

    private void Start()
    {
        Gems = FindObjectsOfType<ItemController>();
        totalGem = Gems.Length;
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        isGameOver = false;
        SignalManager.Instance.ConnectSignal(SetGameOverKey, SetGameOver);
        SignalManager.Instance.ConnectSignal(SetGameClearKey, SetGameClear);
    }

    void Update()
    {
        if (isGameOver) return;
        
        timeElapsed += Time.deltaTime;
        timerText.text = FormatTime(timeElapsed);

        if(gameOverPanel==null)
        gameOverPanel=GameObject.Find("GameOver");
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public bool GemCheck(int collectedGem, int totalGem)
    {
        gemNum.text = ($"{collectedGem}/{totalGem}");
        if (collectedGem < totalGem)
        {
            Debug.Log("너무 적습니다.");
            gemCheck.text = ($"Failed");
            return false;
        }
        else
        {
            Debug.Log("딱 적당히 모았습니다.");
            gemCheck.text = ($"Success");
            return true;
        }
    }
    public bool TimeCheck(float usedTime, float missionTime)
    {
        string textTime = usedTime.ToString("F2");
        timeNum.text = ($"{textTime}/{missionTime}");
        if (usedTime <= missionTime)
        {
            Debug.Log("적당히 빨랐습니다.");
            timeCheck.text = ($"Success");
            return true;
        }
        else
        {
            Debug.Log("느렸습니다.");
            timeCheck.text = ($"Failed");
            return false;
        }
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
    
    public void SetGameClear(object[]args)
    {
        string name=SceneManager.GetActiveScene().name;
        int n = int.Parse(name.Replace("stage", ""));

        bool gemClear=GemCheck(GameManager.Instance.Score, totalGem);
        bool timeClear=TimeCheck(GameManager.Instance.PassedTIme, missionTime);
        if (gemClear&& timeClear)
        {
            //stageinfo stageclear = new stageinfo(true, 3, n);
        }
        else
        {
            //stageinfo stageclear = new stageinfo(true, 2, n);
        }
        //signalmanager.instance.EmitSignal("StageClear", stageinfo);
        gameClearPanel.SetActive(true);
        isGameOver = true;
        SignalManager.Instance.DisconnectSignal(SetGameClearKey, SetGameClear);
    }

    public void Restart()
    {
        bool isPaused = UIManager.Instance.IsPaused;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (isPaused)
            UIManager.Instance.PauseGame();
    }

    public void ExitToStageSelect()
    {
        UIManager.Instance.LoadScene("StartScene");
    }
}