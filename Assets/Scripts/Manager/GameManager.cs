using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    ExitController[] doors;

    [SerializeField] float passedTime;
    public float PassedTIme { get {  return passedTime; } }

    [SerializeField] int score;
    public int Score { get { return score; } }

    public delegate void GameClearAction();
    public GameClearAction OnGameClear;
    public delegate void GameOverAction();

    public const string GameOverkey = "GameOver";
    public const string GameClearKey = "GameClear";

    private void Awake()
    {
        OnGameClear = GameClearCheck;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        doors = FindObjectsOfType<ExitController>();


        GameOverAction OnGameOver;

        SignalManager.Instance.ConnectSignal(GameOverkey, GameOver);
        SignalManager.Instance.ConnectSignal(GameClearKey, GameClear);
    }

    private void Update()
    {
        passedTime += Time.deltaTime;
    }

    public void GameClearCheck()
    {
        bool isAllOpen = doors.All(door => door.isOpen);

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
        SignalManager.Instance.DisconnectSignal("GameOver",GameOver);
        SignalManager.Instance.EmitSignal(GameUI.SetGameOverKey);
    }

    public void GameClear(object[] args)
    {
        SignalManager.Instance.DisconnectSignal("GameClear", GameClear);
        SignalManager.Instance.EmitSignal(GameUI.SetGameClearKey);
    }

}
