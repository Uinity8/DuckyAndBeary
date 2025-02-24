using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    private string sceneName;
    public string SceneName { get => sceneName; set => sceneName = value; }

    private StageState state;
    public StageState State { get => state; set => state = value; }

    public StageData(string _sceneName, StageState _state)
    {
        sceneName = _sceneName;
        state = _state;
    }
}
