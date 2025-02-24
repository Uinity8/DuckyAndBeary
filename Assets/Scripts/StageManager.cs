using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageState
{
    NotOpen,
    NotClear,
    Clear,
    PerfectClear
}

public class StageManager : MonoBehaviour
{
    private const string StageKey = "Stage";
    public static Dictionary<int, StageData> Stages = new Dictionary<int, StageData>
    {
        { 1, new StageData("Stage1", StageState.NotClear) },
        { 2, new StageData("Stage2", StageState.NotOpen) },
        { 3, new StageData("Stage3", StageState.NotOpen) }
    };

    private void Awake()
    {
        LoadStageStates();
    }

    private void LoadStageStates()
    {
        foreach (int key in Stages.Keys)
        {
            Stages[key].State = (StageState)PlayerPrefs.GetInt(StageKey + key, (int)Stages[key].State);
        }
    }


    public static StageState GetStageState(int stageNumber)
    {
        return Stages.ContainsKey(stageNumber) ? Stages[stageNumber].State : StageState.NotOpen;
    }

    public static void SetStageState(int stageNumber, StageState state)
    {
        if (Stages.ContainsKey(stageNumber))
        {
            Stages[stageNumber].State = state;
            PlayerPrefs.SetInt(StageKey + stageNumber, (int)state);
            PlayerPrefs.Save();
        }
    }

    public static void LoadStage(int stageNumber)
    {
        if (Stages.ContainsKey(stageNumber) && Stages[stageNumber].State != StageState.NotOpen)
        {
            SceneManager.LoadScene(Stages[stageNumber].SceneName);
        }
    }
}
