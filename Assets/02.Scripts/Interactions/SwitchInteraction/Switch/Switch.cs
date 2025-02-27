using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : MonoBehaviour
{
     bool isActive;
    bool preIsActive;

    public Action<bool> OnActive;
    public AudioClip switchOn;

    void Update()
    {
        CheckActive();
    }

    void CheckActive()
    {
        isActive = IsActive();
        
        if (preIsActive != isActive)
        {
            SoundManager.PlayClip(switchOn);
            Debug.Log(isActive);
            preIsActive = isActive;
            OnActive?.Invoke(isActive);
        }
    }

    protected abstract bool IsActive();
}