using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [Header ("SwitchInfo")]
    //(Red : 0 , Blue : 1)
    [SerializeField] private int switchColorID;
    public int SwitchColorID { get => switchColorID; }

    //(Lever : 0 / Button : 1)
    [SerializeField] private int switchTypeID;
    public int SwitchTypeID { get => switchTypeID; }

    public virtual bool ActiveSwitch() { return false; }


}
