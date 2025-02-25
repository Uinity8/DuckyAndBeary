using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageState
{
    NotOpen,
    NotClear,
    Clear,
    PerfectClear
}

public class StageStateHandeler : MonoBehaviour
{
    public static Dictionary<int, StageInfo> Stages = new Dictionary<int, StageInfo>
    {
        { 1, new StageInfo("Stage1", StageState.NotClear) },
        { 2, new StageInfo("Stage2", StageState.NotOpen) },
        { 3, new StageInfo("Stage3", StageState.NotOpen) }
    };

    //스테이지 상태불러오기
    public static StageState GetStageState(int stageNumber)
    {
        return Stages.ContainsKey(stageNumber) ? Stages[stageNumber].State : StageState.NotOpen;
    }

    //스테이지 불러오기
    public static void LoadStage(int stageNumber)
    {
        if (Stages.ContainsKey(stageNumber) && Stages[stageNumber].State != StageState.NotOpen)
        {
            SceneManager.LoadScene(Stages[stageNumber].SceneName);
        }
    }
}

