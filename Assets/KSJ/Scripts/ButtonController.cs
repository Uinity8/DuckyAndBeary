using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : InteractionHandler
{
    [SerializeField] private Transform button;
    public Transform Button { get => button; }
   
    private float buttonPosition;

    //Button 위치 값에 따른 bool 값 반환
    public override bool ActiveSwitch()
    {
        buttonPosition = button.localPosition.y;

        return buttonPosition < 0;
    }
}
