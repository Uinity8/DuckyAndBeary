using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float missionTime;
    public float MissionTime{ get { return missionTime; } }
    [SerializeField] float passedTime;
    public float PassedTIme { get {  return passedTime; } }
    private int totalGem;
    public int TotalGem {  get { return totalGem; } }

    [SerializeField] int score;
    public int Score { get { return score; } }
    private const int NumForClear = 2;
    private int OpenExitDorCount = 0;
    ItemController[] Gems;


    static public GameManager instance;
    SignalManager signalManager;

    public GameStage[] stageInfo = new GameStage[0];

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                Instantiate(go);
                instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake()
    {
        signalManager = SignalManager.Instance;
    }
    
    private void Start()
    {
        passedTime = 0;
        Gems = FindObjectsOfType<ItemController>();
        totalGem = Gems.Length;
        signalManager.ConnectSignal(SignalKey.OpenDoor, OnOpenExitDoor);
        signalManager.ConnectSignal(SignalKey.CloseDoor, OnCloseExitDoor);
    }

    private void Update()
    {
        passedTime += Time.deltaTime;
    }
    
    public void AddScore(int value)
    {
        score += value;
    }

    public void Clear()
    {
        Debug.Log("Clear 신호 받음");
        string sceneName = SceneManager.GetActiveScene().name;
        string num = sceneName.Replace("Stage", "");
        Debug.Log($"{num}");
        int n = int.Parse(num);

        bool gemClear = (Score == totalGem);
        bool timeClear = (PassedTIme <= missionTime);
        int clearedValue = gemClear && timeClear ? 3 : 2;

        GameStage stageClear = new GameStage(n, true, clearedValue);

        Debug.Log($"값 생성 완료, 저장된 값:{stageClear.StageIndex}");
        stageInfo[stageClear.StageIndex] = stageClear;
        Debug.Log($"값 저장 완료, 저장된 값:{stageInfo[stageClear.StageIndex].StageIndex}");
    }

    public void OnDestroy()
    {
        signalManager.DisconnectSignal(SignalKey.OpenDoor, OnOpenExitDoor);
        signalManager.DisconnectSignal(SignalKey.CloseDoor, OnCloseExitDoor);
    }

    void OnOpenExitDoor(object sender)
    {
        OpenExitDorCount++;
        if (OpenExitDorCount >= NumForClear)
        {
            SignalManager.Instance.EmitSignal(SignalKey.GameClear);
            Clear();
        }
    }
    void OnCloseExitDoor(object sender)
    {
        OpenExitDorCount--;
        
        
    }

}
