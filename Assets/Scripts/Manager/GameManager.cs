using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ExitController[] doors;

    [SerializeField] float passedTime;
    public float PassedTIme { get {  return passedTime; } }

    [SerializeField] int score;
    public int Score { get { return score; } }

    public delegate void GameClearAction();
    public GameClearAction OnGameClear;
    public delegate void GameOverAction();
    private bool isAllOpen;

    public const string GameOverkey = "GameOver";
    public const string GameClearKey = "GameClear";
    public const string StageClear = "StageClear";

    static public GameManager instance;

    public GameStage[] stageInfo = new GameStage[0];

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Start()
    {
        Debug.Log("게임 스타트 문다시열어줘");
        isAllOpen = false;
        doors = FindObjectsOfType<ExitController>();
        OnGameClear = GameClearCheck;


        GameOverAction OnGameOver;

        SignalManager.Instance.ConnectSignal(GameOverkey, GameOver);
        SignalManager.Instance.ConnectSignal(GameClearKey, GameClear);
        //SignalManager.Instance.ConnectSignal(StageClear, Clear);
    }

    private void Update()
    {
        passedTime += Time.deltaTime;
    }

    public void GameClearCheck()
    {
        isAllOpen = doors.All(door => door.isOpen);

        Debug.Log(isAllOpen?"Game Clear" : "Not Yet");  
        if (isAllOpen)
        {
            SignalManager.Instance.EmitSignal(GameClearKey);
        }
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public void GameOver(object[] args)
    {
        Debug.Log("GameOver");
        SignalManager.Instance.EmitSignal(GameUI.SetGameOverKey);
    }

    public void GameClear(object[] args)
    {
        Debug.Log("GameClear 신호");
        SignalManager.Instance.EmitSignal(GameUI.SetGameClearKey);
    }

    public void Clear(GameStage stageClear)
    {
        stageInfo[stageClear.StageIndex] = stageClear;

        Debug.Log($"값 저장 완료, 저장된 값:{stageInfo[stageClear.StageIndex].StageIndex}");
    }

    //public void OnDestroy()
    //{
    //    SignalManager.Instance.DisconnectSignal("GameOver", GameOver);
    //    SignalManager.Instance.DisconnectSignal("GameClear", GameClear);
    //}
}
