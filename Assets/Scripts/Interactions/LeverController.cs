using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : InteractionHandler
{
    [SerializeField] private Transform lever;
    public Transform Lever { get => lever; }

    private float leverAngel;

    //���� ȸ�� ���� ���� bool �� ��ȯ
    public override bool ActiveSwitch()
    {
        leverAngel = lever.eulerAngles.z;
        return leverAngel > 180;
    }
}
