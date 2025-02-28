using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : Switch
{
    [SerializeField] private Transform lever;
    public Transform Lever { get => lever; }

    private float leverAngel;

    protected override bool IsActive()
    {
        leverAngel = lever.eulerAngles.z;
        return leverAngel > 180;
    }
    
}
