using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SignalKey
{
    OpenDoor,
    CloseDoor,
    GameClear,
    GameOver,
    GamePaused,
    GameResumed,
}


public class SignalManager : MonoBehaviour
{
    static SignalManager instance;
    
    public static SignalManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SignalManager");
                instance = go.AddComponent<SignalManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private Dictionary<SignalKey, Action<object[]>> signals = new();

    public void ConnectSignal(SignalKey signalKey, Action<object[]> action)
    {
        if(!signals.TryAdd(signalKey, action))
        {
            signals[signalKey] += action;
        }
    }

    public void DisconnectSignal(SignalKey signalKey, Action<object[]> action)
    {
        if (signals.ContainsKey(signalKey))
        {
            signals[signalKey] -= action;
            if(signals[signalKey] == null)     //모든 리스터가 제거되면 삭제
                signals.Remove(signalKey);
        }
    }

    public void EmitSignal(SignalKey signalKey, params object[] args)
    {
        if (signals.ContainsKey(signalKey))
        {
            signals[signalKey].Invoke(args);
        }
    }
}
