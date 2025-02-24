using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private Dictionary<string, Action<object[]>> signals = new();

    public void ConnectSignal(string signalName, Action<object[]> action)
    {
        if(!signals.TryAdd(signalName, action))
        {
            signals[signalName] += action;
        }
    }

    public void DisconnectSignal(string signalName, Action<object[]> action)
    {
        if (signals.ContainsKey(signalName))
        {
            signals[signalName] -= action;
            if(signals[signalName] == null)     //모든 리스터가 제거되면 삭제
                signals.Remove(signalName);
        }
    }

    public void EmitSignal(string signalName, params object[] args)
    {
        if (signals.ContainsKey(signalName))
        {
            signals[signalName].Invoke(args);
        }
    }
}
