using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : MonoBehaviour
{
     bool isActive;
    bool preIsActive;

    public Action<bool> OnActive;


    void Update()
    {
        CheckActive();
    }

    void CheckActive()
    {
        isActive = IsActive();
        
        if (preIsActive != isActive)
        {
            Debug.Log(isActive);
            preIsActive = isActive;
            OnActive?.Invoke(isActive);
        }
    }

    protected abstract bool IsActive();
}