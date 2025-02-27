using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : Switch
{
    [Header("Button")]
    [Range(0.1f, 0.2f)][SerializeField] private float sensitivity = 0.15f;
    
    [Header("Chamber")]
    [SerializeField] private Transform chamber;
   
    private Vector2 initialPosition;

    private void Start()
    {
        initialPosition = chamber.localPosition;
    }

    protected override bool IsActive()
    {
        return  chamber.localPosition.y <=  initialPosition.y - sensitivity;
    }
}
