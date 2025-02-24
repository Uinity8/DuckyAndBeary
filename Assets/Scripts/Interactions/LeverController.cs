using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : InteractionHandler
{
    [SerializeField] private Transform lever;
    public Transform Lever { get => lever; }

    private float leverAngel;

    //레버 회전 값에 따른 bool 값 반환
    public override bool ActiveSwitch()
    {
        leverAngel = lever.eulerAngles.z;
        return leverAngel > 180;
    }
}
