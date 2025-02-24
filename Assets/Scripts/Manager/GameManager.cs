using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    ExitController[] doors;

    [SerializeField] float passedTime;

    [SerializeField] int score;

    public delegate void GameClearAction();
    public GameClearAction OnGameClear;
    public delegate void GameOverAction();

    public const string GameOverkey = "GameOver"; 

    private void Awake()
    {
        if(Instance == null)
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

        OnGameClear = GameClearCheck;

        GameOverAction OnGameOver;

        SignalManager.Instance.ConnectSignal(GameOverkey, GameOver);
    }

    private void Update()
    {
        passedTime += Time.deltaTime;
    }

    public void GameClearCheck()
    {
        bool isAllOpen = doors.All(door => door.isOpen);

        Debug.Log(isAllOpen?"Game Clear" : "Not Yet");
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
}
